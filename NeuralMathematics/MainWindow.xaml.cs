﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfMath;
using WpfMath.Controls;
using Maths;
using PersistantModel;
using Interfaces;

namespace NeuralMathematics
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            GrobnerPolynome g = new GrobnerPolynome(7);
            MessageBox.Show(g.PolynomeAsFunction.ToString());
            MessageBox.Show(g.GetBaseY(4242432).ToString());
            MessageBox.Show(g.GetBaseX(3).ToString());
            string res = string.Empty;
            foreach (uint s in g.TrianglePascal(10))
            {
                res += s.ToString() + " ";
            }
            MessageBox.Show(res);
            MessageBox.Show(g.BinomialLaw(3, "U", "v").ToString());
            res = string.Empty;
            foreach (IArithmetic a in g.PolynomeAsFunction.Select(("X").ToArithmetic()))
            {
                res += a.ToString() + " ";
            }
            MessageBox.Show(res);
            InitializeComponent();
        }


        /// <summary>
        /// When the main windows was loaded
        /// </summary>
        /// <param name="sender">source</param>
        /// <param name="r">args</param>
        private void doc_Loaded(object sender, RoutedEventArgs r)
        {
            try
            {
                Applicatif.ButtonClicked += Applicatifs_ButtonClicked;
                this.doc.Document = Applicatif.Menu();
                this.doc.UpdateLayout();
                this.WindowState = WindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// When a button was clicked
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">args</param>
        private void Applicatifs_ButtonClicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            switch(b.Name)
            {
                case "Test":
                    this.doc.Document = Applicatif.Tests();
                    this.doc.UpdateLayout();
                    break;
                case "Num":
                    this.doc.Document = Applicatif.TestNumericalPolynome2();
                    this.doc.UpdateLayout();
                    break;
                case "P2":
                    this.doc.Document = Applicatif.SolvePolynome2();
                    this.doc.UpdateLayout();
                    break;
                case "T2":
                    this.doc.Document = Applicatif.TestPolynome2();
                    this.doc.UpdateLayout();
                    break;
                case "TF3":
                    this.doc.Document = Applicatif.Differential3();
                    this.doc.UpdateLayout();
                    break;
                case "P3":
                    this.doc.Document = Applicatif.SolvePolynome3();
                    this.doc.UpdateLayout();
                    break;
                case "F":
                    this.doc.Document = Applicatif.Differential();
                    this.doc.UpdateLayout();
                    break;
                case "Tab":
                    this.doc.Document = Applicatif.TabularPolynome2();
                    this.doc.UpdateLayout();
                    break;
                case "GoBack":
                    this.doc.Document = Applicatif.Menu();
                    this.doc.UpdateLayout();
                    break;
                case "Sum":
                    this.doc.Document = Applicatif.PolynômeBase10();
                    this.doc.UpdateLayout();
                    break;
                case "Close":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// Size Changed
        /// </summary>
        /// <param name="sender">source</param>
        /// <param name="e">args</param>
        private void doc_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.doc.UpdateLayout();
        }

        /// <summary>
        /// When state changed
        /// </summary>
        /// <param name="sender">source</param>
        /// <param name="e">args</param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            this.doc.UpdateLayout();
        }
    }
}
