﻿using System;
using System.Collections.Generic;
using Simple.Patterns;
using Simple.Remoting.Channels;

namespace Simple.Remoting
{
    public class ChannelSelector
    {
        Dictionary<string, IChannelHandler> _handlers = new Dictionary<string, IChannelHandler>();
        public ChannelSelector()
        {
            AddHandler(new HttpChannelHandler());
            AddHandler(new TcpChannelHandler());
            AddHandler(new IpcChannelHandler());
        }
        protected void AddHandler(IChannelHandler handler)
        {
            _handlers[handler.Scheme] = handler;
        }

        public IChannelHandler GetHandler(Uri uri)
        {
            try
            {
                return _handlers[uri.Scheme];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Invalid uri: " + uri);
            }
        }
    }
}
