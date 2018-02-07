namespace Graphics.Animation
{
    public class AnimationTransition
    {
        
        /*nested types*/
        public class Condition
        {
            /*what to check ?*/
            public enum eConditionType
            {
                Greater,
                Lesser,
                Equal
            }

            public eConditionType ConditionType { get; set; }
            public string Attribute { get; set; }
            public object Value { get; set; }

            public Condition(eConditionType ConditionType, string Attribute, object Value)
            {
                this.ConditionType = ConditionType;
                this.Attribute = Attribute;
                this.Value = Value;
            }

        }

        /*member*/
        public AnimationState TranslateInto { get; set; }
        public Condition TransitionCondition { get; set; }

        /*ctor*/
        public AnimationTransition(Condition TransitionCondition, AnimationState TranslateInto)
        {
            this.TransitionCondition = TransitionCondition;
            this.TranslateInto = TranslateInto;
        }


        /*check if transistion is possible*/
        public bool CanTranslateInto(Animation Animation)
        {
            object attributeValue = Animation.GetAttribute(TransitionCondition.Attribute);
            if (attributeValue  != null && attributeValue.GetType() == typeof(float) && TransitionCondition.Value.GetType() == typeof(float))
            {
                if (TransitionCondition.ConditionType == Condition.eConditionType.Equal)
                    return (float)attributeValue == (float)TransitionCondition.Value;
                else if (TransitionCondition.ConditionType == Condition.eConditionType.Greater)
                    return (float)attributeValue > (float)TransitionCondition.Value;
                else if (TransitionCondition.ConditionType == Condition.eConditionType.Lesser)
                    return (float)attributeValue < (float)TransitionCondition.Value;
            }

            return false;
        }

    }
}
