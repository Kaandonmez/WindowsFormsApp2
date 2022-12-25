using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp2
{

    public partial class Form1 : Form
    {

        //Create regex to detect if the string is empty
        Regex regex = new Regex(@"^\s*$");

        //Create regx to detect \r\n
        Regex regex2 = new Regex(@"\r\n");




        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //clean richTextBox1
            richTextBox1.Text = "";

            //read dictionary from test1.txt
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] lines = System.IO.File.ReadAllLines("test1.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                dict.Add(parts[0], parts[1]);
            }

            //check every line in textbox1 for a match in the dictionary if found write to richTextBox1 , if not write not found to richTextBox1
            string[] lines2 = textBox1.Lines;
            foreach (string line in lines2)
            {



                //if textbox1 line is empty delete that line in textbox1
                if (regex.IsMatch(line) || line == "" || line == " " || regex2.IsMatch(line))
                {

                    var array = textBox1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    textBox1.Text = string.Join(Environment.NewLine, array);


                }



                else
                {
                    if (dict.ContainsKey(line))
                    {
                        richTextBox1.Text += dict[line] + Environment.NewLine;
                    }
                    else
                    {
                        richTextBox1.Text += "Not found" + Environment.NewLine;
                    }
                }



            }

            //paint every "Not found" in red in richTextBox1
            //int index = 0;
            //while (index < richTextBox1.Text.LastIndexOf("Not found"))
            //{
            //    richTextBox1.Find("Not found", index, richTextBox1.TextLength, RichTextBoxFinds.None);
            //    richTextBox1.SelectionColor = Color.Red;
            //    index = richTextBox1.Text.IndexOf("Not found", index) + 1;
            //}
           
            //delete last \n in richTextbox1
            richTextBox1.Text = richTextBox1.Text.TrimEnd('\n');
            
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //set linklabel1 to textbox1 line count
            linkLabel1.Text = "Lines: "+textBox1.Lines.Length.ToString();
        }

        //scroll same time in both richtextbox and textbox
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = richTextBox1.SelectionStart;
            textBox1.SelectionLength = richTextBox1.SelectionLength;
            textBox1.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //add to the dictionary from textbox1 and richTextbox1, check for conflicts
            //key is textbox1 and value is richTextBox1
            //the dictionary is in test1.txt split by =
            //if there is a conflict ask the user if he wants to overwrite the value
            //if the user says yes overwrite the value
            //if the user says no do nothing

            //read dictionary from test1.txt
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] lines = System.IO.File.ReadAllLines("test1.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                dict.Add(parts[0], parts[1]);
            }

            

            
            //check if textbox1 and richTextBox1 have the same number of lines
           if (textBox1.Lines.Length != richTextBox1.Lines.Length)
            {
                MessageBox.Show("Textbox1 and RichTextbox1 have different number of lines");
            }

            //check if textbox1 and richTextBox1 have the same number of lines
            else if (textBox1.Lines.Length == richTextBox1.Lines.Length)
            {
                //check if there is a conflict
                for (int i = 0; i < textBox1.Lines.Length; i++)
                {
                    if (dict.ContainsKey(textBox1.Lines[i]))
                    {
                        //ask the user if he wants to overwrite the value, show the value that will be overwritten, show the new value and show the key
                        DialogResult dialogResult = MessageBox.Show("Do you want to overwrite the value? " + Environment.NewLine + "Key: " + textBox1.Lines[i] + Environment.NewLine + "Old value: " + dict[textBox1.Lines[i]] + Environment.NewLine + "New value: " + richTextBox1.Lines[i], "Overwrite value", MessageBoxButtons.YesNo);
                        
                        if (dialogResult == DialogResult.Yes)
                        {
                            //overwrite the value
                            dict[textBox1.Lines[i]] = richTextBox1.Lines[i];
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //do nothing
                        }
                    }
                    else
                    {
                        //add to the dictionary
                        dict.Add(textBox1.Lines[i], richTextBox1.Lines[i]);
                    }
                }

                //write the dictionary to test1.txt
                System.IO.File.WriteAllText("test1.txt", string.Empty);
                foreach (KeyValuePair<string, string> entry in dict)
                {
                    System.IO.File.AppendAllText("test1.txt", entry.Key + "=" + entry.Value + Environment.NewLine);
                }
            }


            //set textbox2 to total words in dictionary
            textBox2.Text = "Total: "+dict.Count.ToString();










        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            //set linklabel2 richTextbox1 line count
            linkLabel2.Text = "Lines: " + richTextBox1.Lines.Length.ToString();
            

        }
    }
}
