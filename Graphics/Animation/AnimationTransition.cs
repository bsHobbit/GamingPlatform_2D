namespace Graphics.Animation
{
    public class AnimationTransition
    {
        
        /*nested types*/
        public struct Condition
        {
            /*what to look for*/
            public enum eConditionType
            {
                Greater,
                Less,
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
        public Condition TransistionCondition { get; set; }


        public bool CanTranslateInto(Animation Animation)
        {
            object attributeValue = Animation.GetAttribute(TransistionCondition.Attribute);
            if (attributeValue.GetType() == typeof(float) && TransistionCondition.Value.GetType() == typeof(float))
            {
                if (TransistionCondition.ConditionType == Condition.eConditionType.Equal)
                    return (float)attributeValue == (float)TransistionCondition.Value;


            }

            return false;
        }

    }
}
