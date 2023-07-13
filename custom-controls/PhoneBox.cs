using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Autosoft_Controls_2017
{
    /// <summary>
    /// Represents a control that allows the user to enter a phone number
    /// </summary>
    [TemplatePart(Name = "PART_AreaCode", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_ThreeDigit", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_FourDigit", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_Extension", Type = typeof(TextBox))]
    public class PhoneBox : Control
    {
        TextBox areaCode;
        TextBox threeDigit;
        TextBox fourDigit;
        TextBox extension;
        public PhoneBox()
        {
            //this.Resources.MergedDictionaries.Add(SharedDictionaryManager.SharedDictionary);
        }

        /// <summary>
        /// Occurs when the template is applied
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            areaCode = base.GetTemplateChild("PART_AreaCode") as TextBox;
            threeDigit = base.GetTemplateChild("PART_ThreeDigit") as TextBox;
            fourDigit = base.GetTemplateChild("PART_FourDigit") as TextBox;
            extension = base.GetTemplateChild("PART_Extension") as TextBox;

            if (areaCode != null)
                areaCode.TextChanged += AreaCode_TextChanged;
            if (threeDigit != null)
                threeDigit.TextChanged += ThreeDigit_TextChanged;
            if (fourDigit != null)
                fourDigit.TextChanged += FourDigit_TextChanged;
            if (extension != null)
                extension.TextChanged += Extension_TextChanged;

            // Ensure all controls are updated
            UpdateControls(Phone, this);
        }

        static PhoneBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhoneBox),
                new FrameworkPropertyMetadata(typeof(PhoneBox)));

            // Register dependency properties
            PhoneProperty = DependencyProperty.Register("Phone", typeof(Phone),
                typeof(PhoneBox), new PropertyMetadata(new Phone(), new PropertyChangedCallback(OnPhoneChanged)));
            ShowExtensionProperty = DependencyProperty.Register("ShowExtension",
                typeof(bool), typeof(PhoneBox));
        }

        private static void OnPhoneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;

            Phone value = (Phone)e.NewValue;
            var p = (PhoneBox)d;
            value = UpdateControls(value, p);
        }

        private static Phone UpdateControls(Phone phone, PhoneBox phoneBox)
        {
            var areaCode = phoneBox.GetTemplateChild("PART_AreaCode") as TextBox;
            var threeDigit = phoneBox.GetTemplateChild("PART_ThreeDigit") as TextBox;
            var fourDigit = phoneBox.GetTemplateChild("PART_FourDigit") as TextBox;
            var extension = phoneBox.GetTemplateChild("PART_Extension") as TextBox;
            if (areaCode != null && phone.AreaCode.HasValue)
            {
                areaCode.Text = phone.AreaCode.ToString();
            }
            if (threeDigit != null && phone.MiddleDigits.HasValue)
            {
                threeDigit.Text = phone.MiddleDigits.ToString();
            }
            if (fourDigit != null && phone.LastDigits.HasValue)
            {
                fourDigit.Text = phone.LastDigits.ToString();
            }
            if (extension != null && phone.Extension.HasValue)
            {
                extension.Text = phone.Extension.ToString();
            }

            return phone;

        }
        private void AreaCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePhone();
        }
        private void ThreeDigit_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePhone();
        }
        private void FourDigit_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePhone();
        }
        private void Extension_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePhone();
        }

        /// <summary>
        /// Updates the phone
        /// </summary>
        private void UpdatePhone()
        {
            if (areaCode != null && int.TryParse(areaCode.Text, out int aC))
            {
                Phone.AreaCode = aC;
            }
            if (threeDigit != null && int.TryParse(threeDigit.Text, out int mD))
            {
                Phone.MiddleDigits = mD;
            }
            if (fourDigit != null && int.TryParse(fourDigit.Text, out int lD))
            {
                Phone.LastDigits = lD;
            }
            if (ShowExtension && extension != null && int.TryParse(extension.Text, out int ext))
            {
                Phone.Extension = ext;
            }
        }

        /// <summary>
        /// Represents the phone number
        /// </summary>
        public Phone Phone
        {
            get { return (Phone)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }

        /// <summary>
        /// Specifies whether the extension box should be shown
        /// </summary>
        public bool ShowExtension
        {
            get { return (bool)GetValue(ShowExtensionProperty); }
            set { SetValue(ShowExtensionProperty, value); }
        }

        /// <summary>
        /// The Phone dependency property
        /// </summary>
        public static DependencyProperty PhoneProperty;

        /// <summary>
        /// The ShowExtension dependency property
        /// </summary>
        public static DependencyProperty ShowExtensionProperty;
    }
    public class Phone
    {
        /// <summary>
        /// Represents the first three digits of the phone number
        /// </summary>
        public int? AreaCode { get; set; }

        /// <summary>
        /// Represents the three middle digits of the phone number
        /// </summary>
        public int? MiddleDigits { get; set; }

        /// <summary>
        /// Represents the last four digits of a phone number
        /// </summary>
        public int? LastDigits { get; set; }

        /// <summary>
        /// Represents the phone number's extension, if applicable.
        /// </summary>
        public int? Extension { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (AreaCode != null)
            {
                builder.Append(AreaCode);
            }
            if (MiddleDigits != null)
            {
                builder.Append(MiddleDigits);
            }
            if (LastDigits != null)
            {
                builder.Append(LastDigits);
            }
            if (Extension != null)
            {
                builder.Append(Extension);
            }
            return builder.ToString();
        }
    }

}
