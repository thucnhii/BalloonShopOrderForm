// Programmer: Nhi Le
// Project: Assignmetn 3
// Date: 04/10/2023
// Description: Form for Bonnie's Balloons Order

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Le_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Declare and initialize class-level constant variables
        private const decimal HOME_DELIVERY = 7.50m;
        private const decimal SINGLE = 9.95m;
        private const decimal HALF_DOZEN = 35.95m;
        private const decimal DOZEN = 65.96m;
        private const decimal EXTRA_ITEM = 9.50m;
        private const decimal PERSONALIZED_MESSAGE = 2.50m;
        private const decimal TAX_RATE = 0.07m;

        // Show events on startup
        private void Form1_Load(object sender, EventArgs e)
        {
            // Show the current date in the delivery date textbox
            deliveryDateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");

            // Choose pick-up as the default options in the delivery information section
            pickupRadioButton.Checked = true;

            // Show the price of home-delivery option
            homeDeliveryPriceLabel.Text = HOME_DELIVERY.ToString("c");

            // Choose single as the default option in the order details section
            singleRadioButton.Checked = true;

            // Display the price for all items.
            singlePriceLabel.Text = SINGLE.ToString("c");
            halfdozenPriceLabel.Text = HALF_DOZEN.ToString("c");
            dozenPriceLabel.Text = DOZEN.ToString("c");
            extraItemPriceLabel.Text = "(" + EXTRA_ITEM.ToString("c") + "per item)";
            personalizedMessagePriceLabel.Text = PERSONALIZED_MESSAGE.ToString("c");

            // Display the tax rate:
            taxLabel.Text = "(" + TAX_RATE.ToString("p") + ")";

            // Try-catch statement to handle error without crashing the program
            // Populate the special occasions and extra boxes.
            try
            {
                PopulateBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Show the limitation of 30 characters for the custom message.
            messageInstructionLabel.Text = "(Message cannot be longer than 30 character)";

            // Disable the instruction label and the text box for the input
            // of custom message unless the checkbox is checked.

           customMessageLabel.Enabled = false;
            messageInstructionLabel.Enabled = false;
            customMessageTextBox.Enabled = false;
        }

        // This method is used to read the two external files.
        private void PopulateBoxes()
        {
            /*Sort the itmes
             * Read the external file
             * Add items in the external file to the combobox
             * Set the default option for the combobox
             */

            // Sort items in the special occasion combo box from a to z
            specialOccasionComboBox.Sorted = true;

            // Read the Occation.txt files
            string occasionValues;
            StreamReader occasionInputFile;
            occasionInputFile = File.OpenText("Occasion.txt");

            // Add all the items from the file to the special occasion combo box
            while (!occasionInputFile.EndOfStream)
            {
                occasionValues = occasionInputFile.ReadLine().ToString();
                specialOccasionComboBox.Items.Add(occasionValues);
            }
            occasionInputFile.Close();

            // Select Anniversary as the default option for the
            // special occasions combobox.
            specialOccasionComboBox.SelectedItem = "Birthday";

            /*Sort the items
             * Read the external file
             * Add items in the external file to the listbox
             */

            // Sort the items in the listbox
            extraListBox.Sorted = true;

            // Read the Extras.txt file.
            string extraValues;
            StreamReader extraInputFile;
            extraInputFile = File.OpenText("Extras.txt");

            // Add all the items from the file to the extra items listbox.
            while (!extraInputFile.EndOfStream)
            {
                extraValues = extraInputFile.ReadLine().ToString();
                extraListBox.Items.Add(extraValues);
            }
            extraInputFile.Close();
        }

        // This method is used to update the subtotal, tax, and total textboxes
        private void UpdateTotals()
        {
            // Try-catch statement to handle errors without crashing the program
            try
            {
                // Declare local variables
                decimal subtotal = 0m;
                decimal salesTax = 0m;
                decimal total = 0m;

                // If single radio button is checked,
                // the subtotal will equal the price for single bundle
                if (singleRadioButton.Checked)
                {
                    subtotal = SINGLE;
                }

                // If half-dozen radio button is checked,
                // the subtotal will equal the price for half-dozen bundle
                if (halfdozenRadioButton.Checked)
                {
                    subtotal = HALF_DOZEN;
                }

                // If dozen radio button is checked,
                // the subtotal will equal the price for dozen bundle
                if (dozenRadioButton.Checked)
                {
                    subtotal = DOZEN;
                }

                // If subtotal will be adjusted based on the number of selected items
                // in the extra item listbox.
                subtotal += extraListBox.SelectedItems.Count * EXTRA_ITEM;

                // If the personalized message checkbox is checked,
                // the instruction, the label, and the textbpx will be enabled.
                if (personalizedcardCheckBox.Checked)
                {
                    subtotal += PERSONALIZED_MESSAGE;
                    customMessageLabel.Enabled = true;
                    messageInstructionLabel.Enabled = true;
                    customMessageTextBox.Enabled = true;
                }
                
                //If the personalized message checkbox is unchecked,
                // the instructions, the label, and the textbox will be disabled,
                // the content in the textbox will be cleared.
                if (!personalizedcardCheckBox.Checked)
                {
                    customMessageLabel.Enabled = false;
                    messageInstructionLabel.Enabled = false;
                    customMessageTextBox.Enabled = false;
                    customMessageTextBox.Text = " ";
                }

                // If the home delivery button is checked,
                // the subtotal will be added the amount of home delivery fee.
                if (homeDeliveryRadioButton.Checked)
                {
                    subtotal += HOME_DELIVERY;
                }

                // Display the subtotal amount
                subtotalLabel.Text = subtotal.ToString("c");

                // Calculate the amount of sales tax
                salesTax = subtotal * TAX_RATE;

                // Display the amount of sales tax
                salesTaxLabel.Text = salesTax.ToString("c");

                // Calculate the amount of total price for the order
                total = subtotal + salesTax;

                // Display the amount of total price for the order
                totalLabel.Text = total.ToString("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Clear method to reset the form to its original state
        private void ResetForm()
        {
            // Reset all the input options
            singleRadioButton.Checked = true;
            specialOccasionComboBox.Text = "Birthday";
            extraListBox.SelectedIndex = -1;
            personalizedcardCheckBox.Checked = false;
            customMessageLabel.Enabled = false;
            messageInstructionLabel.Enabled = false;
            customMessageTextBox.Enabled = false;
            customMessageTextBox.Text = " ";
            deliveryDateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
            pickupRadioButton.Checked = true;
            titleComboBox.Text = " ";
            firstNameTextBox.Text = " ";
            lastNameTextBox.Text = " ";
            streetTextBox.Text = " ";
            cityTextBox.Text = " ";
            stateMaskTextBox.Text = " ";
            zipMaskedTextBox.Text = " ";
            phoneMaskedTextBox.Text = " ";
            singleRadioButton.Focus();
        }

        private void displaySummaryButton_Click(object sender, EventArgs e)
        {
            // If the first name, last name, and phone number is completed, display the order summary.
            // If the first name, last name and phone number is left blank or incomplete,
            // display specific error message for each error.
            try
            {
                if (firstNameTextBox.Text!= " ")
                {
                    if (lastNameTextBox.Text!= " ")
                    {
                        if (phoneMaskedTextBox.Text!= " ")
                        {
                            // Declare variables for the messagebox
                            string header, name, street, address, phone, deliveryDate,
                                deliveryType, bundle, occasion, customMessage, subtotal, salesTax, total;
                            
                            // Header
                            header = "Bonnie's Ballons Order Summary";

                            // Title, first name and last name of the customer
                            name = titleComboBox.Text + " "+
                                firstNameTextBox.Text + " " +
                                lastNameTextBox.Text;

                            // Street address of the customer
                            street = streetTextBox.Text;

                            // City, state, and zip addess of the customer
                            address = cityTextBox.Text + " " +
                                stateMaskTextBox.Text + " " +
                                zipMaskedTextBox.Text;

                            // Phone number of the customer
                            phone = phoneMaskedTextBox.Text;

                            // Delivery date of the customer
                            deliveryDate = deliveryDateMaskedTextBox.Text;

                            // Delivery type by pickup or home delivery.
                            if (pickupRadioButton.Checked)
                            {
                                deliveryType = pickupRadioButton.Text;
                            }
                            else
                            {
                                deliveryType = homeDeliveryRadioButton.Text;
                            }

                            // Bundle size
                            // If the single button is checked, bundle size is single.
                            if (singleRadioButton.Checked)
                            {
                                bundle = singleRadioButton.Text;
                            }

                            // If the half dozen button is checked, bundle size is half dozen
                            else if (halfdozenRadioButton.Checked)
                            {
                                bundle = halfdozenRadioButton.Text;
                            }

                            // If the dozen button is checked, bundle size is dozen.
                            else
                            {
                                bundle = dozenRadioButton.Text;
                            }

                            // Occasion
                            occasion = specialOccasionComboBox.SelectedText.ToString();

                            // Extras
                            // This variable represent the number of selected items in the extra item box.
                            int size = extraListBox.SelectedItems.Count;

                            // This variable create an array with the number of size equals to
                            // the number of selected items
                            int[] index = new int[size];

                            // This variable holds the selected extra items
                            string extraItems = string.Empty;

                            // If at least one option is chosen in the extra item box,
                            // a loop is created to assign each selected items to the string variable "extraItems"
                            if (extraListBox.SelectedIndex != -1)
                            {
                                for (int i = 0; i < size; i++)
                                {
                                    // Get the index of the selected items
                                    index[i] = extraListBox.SelectedIndices[i];

                                    // Add the items associated with the index to the variable extraItems.
                                    extraItems += "\n" + extraListBox.Items[index[i]].ToString();
                                }
                            }

                            // If no extra items are selected, assigned a blank to the variable
                            else
                            {
                                extraItems = " ";
                            }

                            // Custom Message
                            customMessage = customMessageTextBox.Text;

                            // Order totals variable
                            subtotal = subtotalLabel.Text;
                            salesTax = salesTaxLabel.Text;
                            total = totalLabel.Text;

                            // The Display Summary messagebox.
                            MessageBox.Show(header + "\n" +
                                name + "\n" +
                                street + "\n" +
                                address + "\n" +
                                phone + "\n" +
                                "Delivery Date: " + deliveryDate + "\n" +
                                "Delivery Type: " + deliveryType + "\n" +
                                "Bundle Size: " + bundle + "\n" +
                                "Special Occasion: " + occasion + "\n" +
                                "Extra Items:" + extraItems + "\n" +
                                "Custom Message: " + customMessage + "\n" +
                                "Subtotal: " + subtotal + "\n" +
                                "Sales Tax: " + salesTax + "\n" +
                                "Total: " + total + "\n",
                                "Order Summary",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            // Use the clear method to clear the form to its original state
                            // after displaying the order summary
                            ResetForm();
                        }
                        // Display the error message when phone number is invalid
                        else
                        {
                            MessageBox.Show("Phone Number is invalid", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            phoneMaskedTextBox.Focus();
                        }
                    }
                    // Display the error message that requires the last name textbox to be filled
                    else
                    {
                        MessageBox.Show("Last name cannot be left blank", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lastNameTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("First name cannot be left blank", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    firstNameTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Reset the form when clicking the clear form
        private void clearFormButton_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        // Click the exit button and a pop-up appears.
        //If yes, close the form. If no, close the message box
        private void exitProgramButton_Click(object sender, EventArgs e)
        {
            // Close the form
            DialogResult dialog = MessageBox.Show("Are you sure you wish to quit?",
                                                    "Quit",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // When the radio button is changed, update the order totals
        private void singleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void halfdozenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void extraListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void personalizedcardCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void pickupRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }
    }
}
