using Graphics.Animation;
using System.Windows.Forms;

namespace Editor
{
    public partial class AnimationAttributeEditor : UserControl
    {
        public delegate void AnimationAttributeEditrEventHandler(AnimationAttributeEditor Sender);
        public event AnimationAttributeEditrEventHandler RemoveAttribute;

        Animation Animation;
        public string CurrentAttributeName { get; private set; }
        
        public AnimationAttributeEditor()
        {
            InitializeComponent();
        }

        public AnimationAttributeEditor(Animation Animation, string Attribute, float Value)
            : this()
        {
            CurrentAttributeName = Attribute;
            textBoxName.Text = Attribute;
            numericUpDownValue.Value = (decimal)Value;

            /*Manage user input*/
            textBoxName.TextChanged += (s, e) =>
            {
                string newAttributeName = textBoxName.Text;
                if (newAttributeName != Attribute)
                {
                    if (Animation.IsFreeAttributeName(newAttributeName))
                    {
                        Animation.UpdateAttributeName(CurrentAttributeName, newAttributeName);
                        CurrentAttributeName = newAttributeName;
                    }
                    else
                        textBoxName.Text = CurrentAttributeName;
                }
                
            };
            numericUpDownValue.ValueChanged += (s, e) =>
            {
                Animation.SetAttribute(CurrentAttributeName, (float)numericUpDownValue.Value);
            };
            buttonRemove.Click += (s, e) => { RemoveAttribute?.Invoke(this); };

        }
    }
}
