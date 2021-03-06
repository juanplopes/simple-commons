﻿using System;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableMemberListBinding : EditableMemberBinding
    {
        // Properties
        public EditableElementInitCollection Initializers
        {
            get;
            set;
        }

        public override MemberBindingType BindingType
        {
            get { return MemberBindingType.ListBinding; }
            set { }
        }

        // Ctors
        public EditableMemberListBinding()
        {
            Initializers = new EditableElementInitCollection();
        }

        public EditableMemberListBinding(MemberListBinding member) : base(member.BindingType, member.Member)
        {
            Initializers = new EditableElementInitCollection();
            foreach (ElementInit e in member.Initializers)
            {
                Initializers.Add(new EditableElementInit(e));
            }
        }

        // Methods
        public override MemberBinding ToMemberBinding()
        {
            return Expression.ListBind(Member, Initializers.GetElementsInit());
        }

    }
}
