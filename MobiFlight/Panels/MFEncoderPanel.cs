﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiFlight.UI.Panels
{
    public partial class MFEncoderPanel : UserControl
    {
        /// <summary>
        /// Gets raised whenever config object has changed
        /// </summary>
        public event EventHandler Changed;

        private Config.Encoder encoder;
        bool initialized = false;

        public MFEncoderPanel()
        {
            InitializeComponent();
            mfLeftPinComboBox.Items.Clear();
            mfRightPinComboBox.Items.Clear();
        }

        public MFEncoderPanel(Config.Encoder encoder, List<byte> FreePins)
            : this()
        {
            // TODO: Complete member initialization
            List<byte> Pin1Pins = FreePins.ToList(); Pin1Pins.Add(Convert.ToByte(encoder.PinLeft)); Pin1Pins.Sort();
            List<byte> Pin2Pins = FreePins.ToList(); Pin2Pins.Add(Convert.ToByte(encoder.PinRight)); Pin2Pins.Sort();

            foreach (byte pin in Pin1Pins) mfLeftPinComboBox.Items.Add(pin);
            foreach (byte pin in Pin2Pins) mfRightPinComboBox.Items.Add(pin);


            // Default standard selected values, next pins available
            if (mfLeftPinComboBox.Items.Count > 1)
            {
                mfLeftPinComboBox.SelectedIndex = 0;
                mfRightPinComboBox.SelectedIndex = 1;
            }

            this.encoder = encoder;
            ComboBoxHelper.SetSelectedItem(mfLeftPinComboBox, encoder.PinLeft);
            ComboBoxHelper.SetSelectedItem(mfRightPinComboBox, encoder.PinRight);
            ComboBoxHelper.SetSelectedItemByIndex(mfEncoderTypeComboBox, int.Parse(encoder.EncoderType));

            textBox1.Text = encoder.Name;
            setValues();

            initialized = true;
        }

        private void value_Changed(object sender, EventArgs e)
        {
            if (!initialized) return;

            setValues();

            if (Changed != null)
                Changed(encoder, new EventArgs());
        }

        private void setValues()
        {
            encoder.PinLeft = mfLeftPinComboBox.Text;
            encoder.PinRight = mfRightPinComboBox.Text;
            encoder.EncoderType = mfEncoderTypeComboBox.SelectedIndex.ToString();
            encoder.Name = textBox1.Text;
        }
    }
}
