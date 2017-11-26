﻿using Interfaces;
using PersistantModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Maths;

namespace NeuralMathematics
{
    /// <summary>
    /// Fonctions qui implémentent les documents (FlowDocument)
    /// </summary>
    public static class Applicatif
    {
        /// <summary>
        /// Evenement sur click des boutons
        /// </summary>
        private static event EventHandler onButtonClick;

        /// <summary>
        /// Event handler for clicked button
        /// </summary>
        public static event EventHandler ButtonClicked
        {
            add
            {
                onButtonClick += value;
            }
            remove
            {
                onButtonClick -= value;
            }
        }


        /// <summary>
        /// Fonction de menu
        /// </summary>
        /// <returns></returns>
        public static FlowDocument Menu()
        {
            Table t = new Table();
            t.Columns.Add(new TableColumn());
            t.Columns.Add(new TableColumn());
            t.Columns.Add(new TableColumn());
            t.Columns.Add(new TableColumn());
            TableRowGroup trg = new TableRowGroup();
            TableRow tr = new TableRow();
            Button b1 = new Button();
            b1.Name = "Polynome2Somme";
            b1.Content = "Polynôme d'ordre 2 - Somme";
            b1.Click += Button_Click;
            SetButtonStyle(b1);
            Button b2 = new Button();
            b2.Name = "Polynome2Difference";
            b2.Content = "Polynôme d'ordre 2 - Difference";
            b2.Click += Button_Click;
            SetButtonStyle(b2);
            Button b3 = new Button();
            b3.Name = "Newton";
            b3.Content = "Polynôme d'ordre 2 et 3 - Formule du binôme de Newton";
            b3.Click += Button_Click;
            SetButtonStyle(b3);
            Button b4 = new Button();
            b4.Name = "Polynome2Produit";
            b4.Content = "Polynôme d'ordre 2 - Produit";
            b4.Click += Button_Click;
            SetButtonStyle(b4);
            Button b6 = new Button();
            b6.Name = "ComputeTransformationCoefficientPolynome2";
            b6.Content = "Solutions d'un polynôme d'ordre 2 par transformation du coefficient";
            b6.Click += Button_Click;
            SetButtonStyle(b6);
            TableCell tc = new TableCell(new BlockUIContainer(b1));
            tr.Cells.Add(tc);
            tc = new TableCell(new BlockUIContainer(b2));
            tr.Cells.Add(tc);
            tc = new TableCell(new BlockUIContainer(b3));
            tr.Cells.Add(tc);
            tc = new TableCell(new BlockUIContainer(b6));
            tr.Cells.Add(tc);
            trg.Rows.Add(tr);
            tr = new TableRow();
            tc = new TableCell(new BlockUIContainer(b4));
            tc.ColumnSpan = 4;
            tr.Cells.Add(tc);
            trg.Rows.Add(tr);
            tr = new TableRow();
            Button b5 = new Button();
            b5.Name = "Close";
            b5.Content = "Quitter";
            b5.Click += Button_Click;
            SetButtonStyle(b5);
            tc = new TableCell(new BlockUIContainer(b5));
            tc.ColumnSpan = 4;
            tr.Cells.Add(tc);
            trg.Rows.Add(tr);
            t.RowGroups.Add(trg);

            FlowDocument fd = new FlowDocument(t);
            Paragraph p = new Paragraph();
            fd.Blocks.Add(p);
            return fd;
        }

        /// <summary>
        /// When click on button 1
        /// </summary>
        /// <param name="sender">source window</param>
        /// <param name="e">args</param>
        private static void Button_Click(object sender, RoutedEventArgs e)
        {
            onButtonClick(sender, e);
        }

        /// <summary>
        /// Set the button style
        /// </summary>
        /// <param name="b">button</param>
        private static void SetButtonStyle(Button b)
        {
            b.Height = 50.0d;
            b.Background = Brushes.AliceBlue;
            b.Foreground = Brushes.Black;
        }

        /// <summary>
        /// Développement polynôme
        /// </summary>
        /// <returns></returns>
        public static FlowDocument Newton()
        {
            Wording w = new Wording("Résolution des polynômes", "La résolution des polynômes est un sujet encore mathématiquement non élucidé");
            SequenceProof s1 = new SequenceProof();
            Texte t1 = new Texte("Degré 2");
            Equal eq1 = new Equal((new Coefficient("a") + new Coefficient("b")) ^ 2.0d,
                                   (new Coefficient("a") ^ 2.0d) + new NumericValue(2.0d) * new Coefficient("a") * new Coefficient("b") + (new Coefficient("b") ^ 2.0d));
            eq1 = eq1.MakeUnique() as Equal;

            Texte t2 = new Texte("Degré 3");
            Equal eq2 = new Equal((new Coefficient("a") + new Coefficient("b")) ^ 3.0d,
                                        new Sum(new Coefficient("a") ^ 3.0d,
                                        new NumericValue(3.0d) * (new Coefficient("a") ^2.0d) * new Coefficient("b"),
                                        new NumericValue(3.0d) * (new Coefficient("b") ^ 2.0d) * new Coefficient("a"),
                                        new Coefficient("b") ^ 3.0d));
            eq2 = eq2.MakeUnique() as Equal;
            s1.Add(t1);
            s1.Add(eq1);
            s1.Add(t2);
            s1.Add(eq2);
            SequenceProof s2 = new SequenceProof();
            Texte tex2 = new Texte("Quelque soit x un nombre réel");
            Equal eqEx1 = new Equal(new UnknownTerm("y"), new Sum(new Coefficient("a") * (new UnknownTerm("x") ^ 2.0d),
                                                                  new Coefficient("b") * "x",
                                                                  new Coefficient("c")));
            eqEx1 = eqEx1.MakeUnique() as Equal;
            s2.Add(tex2);
            s2.Add(eqEx1);

            SequenceProof s3 = new SequenceProof();
            Texte tex3 = new Texte("Quelque soit x un nombre réel");
            Equal eqEx3 = new Equal(new UnknownTerm("y") / 'a',
                                    new Sum(new UnknownTerm("x") ^ 2.0d, (new Coefficient("b") / 'a') * "x",
                                    new Coefficient("c") / 'a'));
            s3.Add(tex3, eqEx3);

            SequenceProof s4 = new SequenceProof();
            Texte tex4 = new Texte("Quelque soit x un nombre réel");

            Equal eqEx4 = new Equal(new UnknownTerm("y") / 'a',
                                    new Sum(new UnknownTerm("x") * (new UnknownTerm("x") + (new Coefficient("b") / 'a')),
                                    new Coefficient("c") / 'a'));
            s4.Add(tex4, eqEx4);

            SequenceProof s5 = new SequenceProof();
            Texte tex5 = new Texte("Forme algébrique somme-produit");
            Equal eqEx5 = new Equal((new UnknownTerm("y") - 'c') / 'a',
                                    new Sum(new UnknownTerm("x") * (new UnknownTerm("x") + (new Coefficient("b") / 'a'))));
            Texte tex6 = new Texte("L'équation obtenue montre une façon de résoudre l'équation avec la formule de Newton. Le résultat obtenu est une différence de deux carrés.");
            s5.Add(tex5, eqEx5, tex6);


            Answer a = new Answer("Expressions avec la formule de Newton", s1);
            Answer b = new Answer("Polynôme ordre 2", s2);
            Answer c = new Answer("Factorisation du coefficient a si non nul", s3);
            Answer d = new Answer("Factorisation du terme inconnu", s4);
            Answer f = new Answer("Coefficient c", s5);
            Exercice e = new Exercice(1, "Formule de Newton", "Décrivez la production de la formule de newton à l'ordre 2 et 3", a);
            Exercice e2 = new Exercice(2, "Polynôme d'ordre 2", "Présentez l'équation d'un polynôme d'ordre 2 noté y en fonction de x", b);
            Exercice e3 = new Exercice(3, "Factorisez le coefficient a", "le coefficient a est toujours non nul", c);
            Exercice e4 = new Exercice(4, "Factorisez le terme x", "le coefficient c est laissé dans la somme", d);
            Exercice e5 = new Exercice(5, "Placez le coefficient c de l'autre côté de l'égalité", "le signe - est adjoint à c", f);
            w.Add(e);
            w.Add(e2);
            w.Add(e3);
            w.Add(e4);
            w.Add(e5);

            FileInfo fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "eq1.bin"));
            TopLevelArithmeticModel t = TopLevelArithmeticModel.Create("test-eq1");
            t.WordingList.Add(w);
            t.Save(fi);
            FlowDocument fd = new FlowDocument();

            Button but = new Button();
            but.Name = "GoBack";
            but.Content = "Retour";
            but.Click += Button_Click;
            SetButtonStyle(but);
            fd.Blocks.Add(new BlockUIContainer(but));

            w.ToDocument(fd);
            return fd;
        }

        /// <summary>
        /// Lecture fichier
        /// </summary>
        /// <returns></returns>
        public static FlowDocument ReloadNewton()
        {
            FileInfo fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "eq1.bin"));
            TopLevelArithmeticModel t = TopLevelArithmeticModel.Load(fi);
            FlowDocument fd = new FlowDocument();
            t.WordingList[0].ToDocument(fd);
            return fd;
        }

        /// <summary>
        /// Résolution polynôme ordre 2
        /// </summary>
        public static FlowDocument ResolutionPolynome2Somme()
        {
            Wording w = new Wording("Résoudre un polynôme d'ordre 2", "Considérons la somme de deux carrés");
            w.Content.Add(new Texte("La somme de deux carrés s'écrit"),
                          (new UnknownTerm("u") ^ 2.0d) + (new UnknownTerm("v") ^ 2.0d));
            w.Content.Add(new Texte("Une équation polynôme de second degré s'écrit"),
                          new Equal(new UnknownTerm("y"), 
                              new Sum(new Coefficient("a") * new UnknownTerm("x") ^ 2.0d,
                                      new Coefficient("b") * "x",
                                      new Coefficient("c"))),
                          new Texte("Je factorise le terme {a}, puis le terme {x}, puis je place le coefficient {c} de l'autre côté de l'égalité", true),
                          new Equal(new UnknownTerm("y") / 'a',
                                    new Sum(new UnknownTerm("x") ^ 2.0d,
                                            new Coefficient("b") / 'a' * "x",
                                            new Coefficient("c") / 'a')),
                          new Texte("J'obtiens un premier indice de la forme algébrique de l'équation du second degré permettant de résoudre"));

            Texte t1 = new Texte("Quelque soient deux nombres réels {u} et {v}", true);
            Sum eq1 = new Sum((new UnknownTerm("u") ^ 2.0d),
                              (new UnknownTerm("v") ^ 2.0d));
            SequenceProof sp1 = new SequenceProof(t1, eq1);

            Texte t2 = new Texte("Quelque soient deux nombres réels {u} et {v}", true);
            Equal eq2 = new Equal(eq1, ((new UnknownTerm("u") + new UnknownTerm("v")) ^ 2.0d) - new NumericValue(2.0d) * new UnknownTerm("u") * new UnknownTerm("v"));
            SequenceProof sp2 = new SequenceProof(t2, eq2);

            Equal eq3 = new Equal(eq1 - (new UnknownTerm("v") ^ 2.0d), (new UnknownTerm("u") ^ 2.0d) + new NumericValue(2.0d) * new UnknownTerm("u") * new UnknownTerm("v"));

            SequenceProof sp3 = new SequenceProof(eq3);

            Texte t4 = new Texte("Quelque soit {x_0} et {x}, deux nombres réels", true);
            Equal eq4u = new Equal(new UnknownTerm("u"), new UnknownTerm("x"));
            Equal eq4v = new Equal(new UnknownTerm("v"), new UnknownTerm("x_0"));
            Texte t40 = new Texte("Je remplace les termes {u} et {v} par leurs équations respectives", true);

            Equal eq41 = new Equal(new Sum(new Power(new UnknownTerm("x"), new NumericValue(2.0d)), new Product(new NumericValue(2.0d), new UnknownTerm("x"), new UnknownTerm("x_0"))),
                                  new Soustraction(new Power(new Addition(new UnknownTerm("x"), new UnknownTerm("x_0")), new NumericValue(2.0d)), new Power(new UnknownTerm("x_0"), new NumericValue(2.0d))));

            Texte t41 = new Texte("Je factorise le terme {x}", true);
            Equal eq42 = new Equal(new Product(new UnknownTerm("x"), new Sum(new UnknownTerm("x"), new Multiplication(new NumericValue(2.0d), new UnknownTerm("x_0")))),
                                  new Soustraction(new Power(new Addition(new UnknownTerm("x"), new UnknownTerm("x_0")), new NumericValue(2.0d)), new Power(new UnknownTerm("x_0"), new NumericValue(2.0d))));
            Texte t42 = new Texte("C'est le produit du terme {x} et de la somme de {x} avec le coefficient {b = 2 * x_0}. Il s'agit donc bien d'un polynôme d'ordre 2.", true);

            SequenceProof sp4 = new SequenceProof(t4, eq4u, eq4v, t40, eq41, t41, eq42, t42);

            Texte t5 = new Texte("Soit l'équation complète avec l'inconnu {x} et le résultat {y}", true);
            Equal eq51 = new Equal(new UnknownTerm("y"), eq42.LeftOperand);
            Texte t51 = new Texte("Le terme {b = 2 * x_0} gêne la résolution simple. Si {x_0=0} alors l'équation devient", true);
            Equal eq52 = new Equal(new UnknownTerm("y"), new Power(new UnknownTerm("x"), new NumericValue(2.0d)));
            Texte t52 = new Texte("Et aussi lorsque {x_0 = b} alors l'équation devient", true);
            Equal eq53 = new Equal(new UnknownTerm("y"), new Addition(new Power(new UnknownTerm("x"), new NumericValue(2.0d)), new Term(2.0d, "b", "x")));

            Texte t53 = new Texte("Avec un peu d'effort, on reconnait qu'il existe un unique nombre {x_0 = b/2} qui donne exactement {y}", true);
            Equal eq54 = new Equal(new UnknownTerm("y"), new Addition(new Power(new UnknownTerm("x"), new NumericValue(2.0d)), new Multiplication(new Coefficient("b"), new UnknownTerm("x"))));


            SequenceProof sp5 = new SequenceProof(t5, eq51, t51, eq52, t52, eq53, t53, eq54);

            SequenceProof sp6 = new SequenceProof();

            Answer a1 = new Answer("Somme de deux carrés", sp1);
            Answer a2 = new Answer("Identité remarquable", sp2);
            Answer a3 = new Answer("Calcul du plus proche carré d'un nombre", sp3);
            Answer a4 = new Answer("Je recherche une équation pour {u} et {v} tel que {x} et {x_0} sont obtenus selon un polynôme d'ordre 2.", true, sp4);
            Answer a5 = new Answer("Recherche de la solution avec une équation de {x_0}", true, sp5);
            Answer a6 = new Answer("Résolution", true, sp6);




            Exercice e1 = new Exercice(1, "Poser la somme de deux carrés", "Choisissez les lettres {u} et {v}", true, a1);
            Exercice e2 = new Exercice(2, "Utilisez une identité remarquable connue pour transformer l'équation en un produit", "Utilisez la formule du binôme de Newton à l'ordre 2", a2);
            Exercice e3 = new Exercice(3, "Rapporter une différence en inversant les termes", "Cela permet de trouver le plus proche carré d'un nombre", a3);
            Exercice e4 = new Exercice(4, "Retrouver l'équation du produit d'un polynôme d'ordre 2", "Poser {x0} et {x}", true, a4);
            Exercice e5 = new Exercice(5, "Trouvez une équation de {x_0} pour éliminer le terme {b = 2 * x_0} gênant", "Chercher pourquoi élimiter ce terme", true, a5);
            Exercice e6 = new Exercice(5, "En déduire les équations solutions du polynôme d'ordre 2", "Trouver, au mieux, deux équations solutions", true, a6);
            w.Add(e1);
            w.Add(e2);
            w.Add(e3);
            w.Add(e4);
            w.Add(e5);
            w.Add(e6);
            FlowDocument fd = new FlowDocument();

            Button but = new Button();
            but.Name = "GoBack";
            but.Content = "Retour";
            but.Click += Button_Click;
            SetButtonStyle(but);
            fd.Blocks.Add(new BlockUIContainer(but));

            w.ToDocument(fd);

            return fd;
        }

        /// <summary>
        /// Résolution polynôme ordre 2
        /// </summary>
        public static FlowDocument ResolutionPolynome2Difference()
        {
            Wording w = new Wording("Résolution du polynôme d'ordre 2", "Considérons la différence de deux carrés");

            Texte t1 = new Texte("Quelque soit u et v, deux nombres réels");
            Soustraction eq1 = new Soustraction(new Power(new UnknownTerm("u"), new NumericValue(2.0d)), new Power(new UnknownTerm("v"), new NumericValue(2.0d)));
            SequenceProof sp1 = new SequenceProof(t1, eq1);

            Texte t2 = new Texte("Quelque soit u et v, deux nombres réels");
            Equal eq2 = new Equal(eq1, new Product(new Addition(new UnknownTerm("u"), new UnknownTerm("v")),
                                       new Soustraction(new UnknownTerm("u"), new UnknownTerm("v"))));
            SequenceProof sp2 = new SequenceProof(t2, eq2);

            Texte t3 = new Texte("Quelque soit x0 et x");
            Equal eq3u = new Equal(new UnknownTerm("u"), new Addition(new UnknownTerm("x_0"), new UnknownTerm("x")));
            Equal eq3v = new Equal(new UnknownTerm("v"), new UnknownTerm("x_0"));
            Equal eq3 = new Equal(new Soustraction(new Power(new Addition(new UnknownTerm("x_0"), new UnknownTerm("x")), new NumericValue(2.0d)),
                                         new Power(new UnknownTerm("x_0"), new NumericValue(2.0d))),
                                         new Product(new Addition(new UnknownTerm("x"), new Product(new NumericValue(2.0d), new UnknownTerm("x_0"))), new UnknownTerm("x")));
            Equal eq31 = new Equal(new Product(new Addition(new UnknownTerm("x"), new Product(new NumericValue(2.0d), new UnknownTerm("x_0"))), new UnknownTerm("x")),
                                               new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")), new Coefficient("a")));
            Texte t32 = new Texte("Le produit est égal à une solution de l'équation d'un polynôme de degré 2 défini par l'équation");
            Equal eq33 = new Equal(new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")),
                                   new Coefficient("a")), new Product(new UnknownTerm("x"),
                                   new Addition(new UnknownTerm("x"), new Division(new Coefficient("b"), new Coefficient("a")))));

            SequenceProof sp3 = new SequenceProof(t3, eq3u, eq3v, eq3, eq31, t32, eq33);

            Texte t4 = new Texte("Par identification");
            Equal eq4 = new Equal(new Product(new NumericValue(2.0d), new UnknownTerm("x_0")), new Division(new Coefficient("b"), new Coefficient("a")));
            Texte t41 = new Texte("D'où une solution pour x0");
            Equal eq42 = new Equal(new UnknownTerm("x_0"), new Division(new Coefficient("b"), new Multiplication(new NumericValue(2.0d), new Coefficient("a"))));

            SequenceProof sp4 = new SequenceProof(t4, eq4, t41, eq42);

            Answer a1 = new Answer("Différence de deux carrés", sp1);
            Answer a2 = new Answer("Identité remarquable", sp2);
            Answer a3 = new Answer("Expression de u et v", sp3);
            Answer a4 = new Answer("Expression de x0", sp4);

            Exercice e1 = new Exercice(1, "Poser la différence de deux carrés", "Choisissez les lettres u et v", a1);
            Exercice e2 = new Exercice(2, "Utilisez l'identité remarquable connue pour exprimer la différence de deux carrés", "", a2);
            Exercice e3 = new Exercice(3, "Supposer que le produit est égal au produit d'un polynôme d'ordre 2", "Poser les équations de u et v", a3);
            Exercice e4 = new Exercice(3, "En déduire une solution numérique pour x0", "Les solutions dépendent de la valeur x0 et y", a4);
            w.Add(e1);
            w.Add(e2);
            w.Add(e3);
            w.Add(e4);

            FlowDocument fd = new FlowDocument();

            Button but = new Button();
            but.Name = "GoBack";
            but.Content = "Retour";
            but.Click += Button_Click;
            SetButtonStyle(but);
            fd.Blocks.Add(new BlockUIContainer(but));

            w.ToDocument(fd);
            return fd;
        }

        /// <summary>
        /// Résolution polynôme ordre 2 par produit
        /// </summary>
        /// <returns>document</returns>
        public static FlowDocument Polynome2Produit()
        {
            Wording w = new Wording("Résolution du polynôme d'ordre 2", "Considérons le produit");

            Texte t1 = new Texte("Equation du second degré");
            Equal eq1 = new Equal(new UnknownTerm("y"), new Sum(new Multiplication(new Coefficient("a"), new Power(new UnknownTerm("x"), new NumericValue(2.0d))),
                                                                new Multiplication(new Coefficient("b"), new UnknownTerm("x")),
                                                                new Coefficient("c")));
            Texte t12 = new Texte("Je factorise le terme a, puis le terme x, puis je place le coefficient c de l'autre côté de l'égalité");
            Equal eq12 = new Equal(new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")), new Coefficient("a")),
                                   new Multiplication(new UnknownTerm("x"), new Addition(new UnknownTerm("x"), new Division(new UnknownTerm("b"), new Coefficient("a")))));

            SequenceProof sp1 = new SequenceProof();
            sp1.Add(t1);
            sp1.Add(eq1);
            sp1.Add(t12);
            sp1.Add(eq12);

            Answer a1 = new Answer("Equation produit", sp1);

            Exercice e1 = new Exercice(1, "Exprimez le polynôme d'ordre 2 sous la forme d'une égalité entre une différence et un produit", "Démarrez à partir de l'équation polynômiale avec des coefficients", a1);

            w.Add(e1);

            FlowDocument fd = new FlowDocument();

            Button but = new Button();
            but.Name = "GoBack";
            but.Content = "Retour";
            but.Click += Button_Click;
            SetButtonStyle(but);
            fd.Blocks.Add(new BlockUIContainer(but));

            w.ToDocument(fd);

            return fd;
        }


        /// <summary>
        /// Résolution polynôme ordre 2
        /// </summary>
        public static FlowDocument ResolutionPolynome3()
        {
            Wording w = new Wording("Résolution du polynôme d'ordre 3", "Considérons la différence ou la somme de deux cubes");

            Texte t1 = new Texte("Quelque soit u et v, deux nombres réels");
            Soustraction eq1 = new Soustraction(new Power(new UnknownTerm("u"), new NumericValue(3.0d)), new Power(new UnknownTerm("v"), new NumericValue(3.0d)));
            SequenceProof sp1 = new SequenceProof(t1, eq1);

            Texte t2 = new Texte("Quelque soit u et v, deux nombres réels");
            Equal eq2 = new Equal(eq1, new Product(new Soustraction(new UnknownTerm("u"), new UnknownTerm("v")),
                                       new Sum(new Power(new UnknownTerm("u"), new NumericValue(2.0d)),
                                               new Multiplication(new UnknownTerm("u"), new UnknownTerm("v")),
                                               new Power(new UnknownTerm("v"), new NumericValue(2.0d))).Transition()));
            SequenceProof sp2 = new SequenceProof(t2, eq2);

            Texte t3 = new Texte("Quelque soit x0, x1 et x");
            Equal eq3 = new Equal(new Soustraction(new Power(new Addition(new UnknownTerm("x_1"), new UnknownTerm("x")), new NumericValue(3.0d)),
                                         new Power(new Addition(new UnknownTerm("x_1"), new UnknownTerm("x_0")), new NumericValue(3.0d))),
                                         new Product(new Soustraction(new UnknownTerm("x"), new UnknownTerm("x_0")),
                                         new Sum(new Power(new Addition(new UnknownTerm("x_1"), new UnknownTerm("x")), new NumericValue(2.0d)),
                                                 new Product(new Addition(new UnknownTerm("x_1"), new UnknownTerm("x")), new Addition(new UnknownTerm("x_1"), new UnknownTerm("x_0"))),
                                                 new Power(new Addition(new UnknownTerm("x_1"), new UnknownTerm("x_0")), new NumericValue(2.0d))).Transition()));
            Texte t31 = new Texte("Je développe la partie droite du produit");
            Equal eq31 = new Equal(new Power(new Addition(new UnknownTerm("x_1"), new UnknownTerm("x")), new NumericValue(2.0d)),
                                   new Sum(new Power(new UnknownTerm("x_1"), new NumericValue(2.0d)),
                                        new Product(new NumericValue(2.0d), new UnknownTerm("x_1"), new UnknownTerm("x")),
                                        new Power(new UnknownTerm("x"), new NumericValue(2.0d))));
            Equal eq32 = new Equal(new Power(new Addition(new UnknownTerm("x_1"), new UnknownTerm("x_0")), new NumericValue(2.0d)),
                                   new Sum(new Power(new UnknownTerm("x_1"), new NumericValue(2.0d)),
                                        new Product(new NumericValue(2.0d), new UnknownTerm("x_1"), new UnknownTerm("x_0")),
                                        new Power(new UnknownTerm("x_0"), new NumericValue(2.0d))));
            Equal eq33 = new Equal(new Product(new Addition(new UnknownTerm("x_1"), new UnknownTerm("x")), new Addition(new UnknownTerm("x_1"), new UnknownTerm("x_0"))),
                                   new Sum(new Power(new UnknownTerm("x_1"), new NumericValue(2.0d)), new Product(new UnknownTerm("x_1"), new UnknownTerm("x_0")), new Product(new UnknownTerm("x"), new UnknownTerm("x_1")), new Product(new UnknownTerm("x"), new UnknownTerm("x_0"))));

            SequenceProof sp3 = new SequenceProof(t3, eq3, t31, eq31, eq32, eq33);

            Texte t4 = new Texte("Par identification");
            Equal eq4 = new Equal(new Product(new NumericValue(2.0d), new UnknownTerm("x_0")), new Division(new Coefficient("b"), new Coefficient("a")));
            Texte t41 = new Texte("D'où une solution pour x0");
            Equal eq42 = new Equal(new UnknownTerm("x_0"), new Division(new Coefficient("b"), new Multiplication(new NumericValue(2.0d), new Coefficient("a"))));

            SequenceProof sp4 = new SequenceProof(t4, eq4, t41, eq42);

            Answer a1 = new Answer("Différence de deux cubes", sp1);
            Answer a2 = new Answer("Identité remarquable", sp2);
            Answer a3 = new Answer("Expression de u et v", sp3);
            Answer a4 = new Answer("Expression de x0", sp4);

            Exercice e1 = new Exercice(1, "Poser la différence de deux cubes", "Choisissez les lettres u et v", a1);
            Exercice e2 = new Exercice(2, "Utilisez l'identité remarquable connue pour exprimer la différence de deux cubes", "", a2);
            Exercice e3 = new Exercice(3, "Supposer que le produit est égal au produit d'un polynôme d'ordre 3", "Poser les équations de u et v", a3);
            Exercice e4 = new Exercice(3, "En déduire une solution numérique pour x0", "Les solutions dépendent de la valeur x0 et y", a4);
            w.Add(e1);
            w.Add(e2);
            w.Add(e3);
            w.Add(e4);

            FlowDocument fd = new FlowDocument();

            Button but = new Button();
            but.Name = "GoBack";
            but.Content = "Retour";
            but.Click += Button_Click;
            SetButtonStyle(but);
            fd.Blocks.Add(new BlockUIContainer(but));

            w.ToDocument(fd);
            return fd;
        }

        /// <summary>
        /// Compute a transformation of the coefficient named as b to resolve 2-polynomial
        /// </summary>
        /// <returns></returns>
        public static FlowDocument ComputeTransformationCoefficientPolynome2()
        {

            Wording w = new Wording("Transformation du coefficient {b} dans un polynôme d'ordre 2", "Calcul des solutions de l'équation du polynôme d'ordre 2", true);
            w.Content.Add(new Texte("Une équation polynôme de second degré s'écrit"),
                new Equal(new UnknownTerm("y"), new Sum(new Multiplication(new Coefficient("a"), new Power(new UnknownTerm("x"), new NumericValue(2.0d))),
                        new Multiplication(new Coefficient("b"), new UnknownTerm("x")),
                        new Coefficient("c"))),
                new Texte("Je factorise le terme {a}, puis le terme {x}, puis je place le coefficient {c} de l'autre côté de l'égalité", true),
                new Equal(new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")), new Coefficient("a")),
                        new Multiplication(new UnknownTerm("x"), new Addition(new UnknownTerm("x"), new Division(new UnknownTerm("b"), new Coefficient("a"))))),
                new Texte("J'obtiens un premier indice de la forme algébrique de l'équation du second degré permettant de résoudre"));

            Texte t1 = new Texte("Quelque soit un réel {x}", true);
            Equal eq1 = new Equal(new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")), new Coefficient("a")),
                                  new Sum(new Power(new UnknownTerm("x"), new NumericValue(2.0d)),
                                          new Multiplication(new Coefficient("b"), new UnknownTerm("x"))));

            Texte t2 = new Texte("Multiplier et diviser par 2 sur le coefficient {b} de l'équation du polynôme d'ordre 2", true);

            Equal eq2 = new Equal(new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")), new Coefficient("a")),
                                  new Sum(new Power(new UnknownTerm("x"), new NumericValue(2.0d)),
                                          new Multiplication(new Division(new Multiplication(new NumericValue(2.0d), new Coefficient("b")), new NumericValue(2.0d)), new UnknownTerm("x"))));

            Texte t3 = new Texte(@"Ajouter et retrancher le terme constant '{\frac{b}{2}}^2'", true, '\'', '\'');

            Equal eq3 = new Equal(new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")), new Coefficient("a")),
                                  new Sum(new Power(new UnknownTerm("x"), new NumericValue(2.0d)),
                                          new Multiplication(new Division(new Multiplication(new NumericValue(2.0d), new Coefficient("b")), new NumericValue(2.0d)), new UnknownTerm("x")),
                                          new Division(new Power(new Coefficient("b"), new NumericValue(2.0d)), new NumericValue(4.0d)),
                                          new Negative(new Division(new Power(new Coefficient("b"), new NumericValue(2.0d)), new NumericValue(4.0d)))));

            Texte t31 = new Texte("L'équation obtenue permet de tirer la soustraction de deux carrés");

            Equal eq31 = new Equal(new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")), new Coefficient("a")),
                                  new Sum(new Power(new Addition(new UnknownTerm("x"), new Division(new Coefficient("b"), new NumericValue(2.0d))), new NumericValue(2.0d)),
                                          new Multiplication(new Division(new Multiplication(new NumericValue(2.0d), new Coefficient("b")), new NumericValue(2.0d)), new UnknownTerm("x")),
                                          new Division(new Power(new Coefficient("b"), new NumericValue(2.0d)), new NumericValue(4.0d)),
                                          new Negative(new Division(new Power(new Coefficient("b"), new NumericValue(2.0d)), new NumericValue(4.0d)))));

            SequenceProof sp1 = new SequenceProof(t1, eq1);
            SequenceProof sp2 = new SequenceProof(t2, eq2);
            SequenceProof sp3 = new SequenceProof(t3, eq3);


            Answer a1 = new Answer("Equation du polynôme d'ordre 2", sp1);
            Answer a2 = new Answer("Multiplication par 2", sp2);
            Answer a3 = new Answer("Ajout d'un terme constant", sp3);

            Exercice e1 = new Exercice(1, "Ecrire l'équation d'un polynôme d'ordre 2", "Choisir les lettres {x} pour l'abscisse et {y} pour l'ordonnée", true, a1);
            Exercice e2 = new Exercice(2, "Multiplier par 2 le terme {b} et diviser par 2", "le nombre 2 s'annule", true, a2);
            Exercice e3 = new Exercice(3, @"Ajouter et retrancher '{\frac{b}{2}}^2'", "le nombre 2 permet de factoriser l'équation", true, '\'', '\'', a3);
            w.Add(e1);
            w.Add(e2);
            w.Add(e3);

            FlowDocument fd = new FlowDocument();

            Button but = new Button();
            but.Name = "GoBack";
            but.Content = "Retour";
            but.Click += Button_Click;
            SetButtonStyle(but);
            fd.Blocks.Add(new BlockUIContainer(but));

            w.ToDocument(fd);
            return fd;
        }

        /// <summary>
        /// Computes image size
        /// </summary>
        /// <returns></returns>
        public static FlowDocument ComputeImageSize()
        {
            FlowDocument fd = new FlowDocument();

            Button but = new Button();
            but.Name = "GoBack";
            but.Content = "Retour";
            but.Click += Button_Click;
            SetButtonStyle(but);
            fd.Blocks.Add(new BlockUIContainer(but));

            Coordinates[] bornes = new Coordinates[2];
            bornes[0] = new Coordinates(-5.0d, -5.0d);
            bornes[1] = new Coordinates(5.0d, 5.0d);
            Maths.Vector v = new Maths.Vector(bornes[0], bornes[1]);
            Coordinates s = new Coordinates(0.1d, 0.1d);
            MovingCoordinates mc = new MovingCoordinates(v, s);
            DistributedTracer2D d = new DistributedTracer2D(mc, 4, 4, 6, new Size(1.0d, 1.0d));
            Addition a = new Addition(new Multiplication(new Coefficient("a"), new Power(new UnknownTerm("x"), new NumericValue(2.0d))),
                                      new Multiplication(new Coefficient("b"), new UnknownTerm("x")));
            a.Let("a", 1.0d);
            a.Let("b", 0.0d);
            d.SetFunction(a);

            ClipBox cb = new ClipBox();
            cb.Tracer = d;
            fd.Blocks.Add(new BlockUIContainer(cb));
            return fd;
        }
    }
}
