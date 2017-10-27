﻿using Interfaces;
using PersistantModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Maths
{
    public static class Applicatifs
    {
        /// <summary>
        /// Développement polynôme
        /// </summary>
        /// <returns></returns>
        public static FlowDocument Newton()
        {
            Wording w = new Wording("Résolution des polynômes", "La résolution des polynômes est un sujet encore mathématiquement non élucidé");
            SequenceProof s1 = new SequenceProof();
            Texte t1 = new Texte("Degré 2");
            Equal eq1 = new Equal(new Power(new Addition(new Coefficient("a"), new Coefficient("b")), new NumericValue(2.0d)),
                                        new Sum(new Power(new Coefficient("a"), new NumericValue(2.0d)), new Product(new NumericValue(2.0d), new Coefficient("a"), new Coefficient("b")), new Power(new Coefficient("b"), new NumericValue(2.0d))));
            eq1 = eq1.MakeUnique() as Equal;

            Texte t2 = new Texte("Degré 3");
            Equal eq2 = new Equal(new Power(new Addition(new Coefficient("a"), new Coefficient("b")), new NumericValue(3.0d)),
                                        new Sum(new Power(new Coefficient("a"), new NumericValue(3.0d)),
                                        new Product(new NumericValue(3.0d), new Power(new Coefficient("a"), new NumericValue(2.0d)), new Coefficient("b")),
                                        new Product(new NumericValue(3.0d), new Coefficient("a"), new Power(new Coefficient("b"), new NumericValue(2.0d))),
                                        new Power(new Coefficient("b"), new NumericValue(3.0d))));
            eq2 = eq2.MakeUnique() as Equal;
            s1.Add(t1);
            s1.Add(eq1);
            s1.Add(t2);
            s1.Add(eq2);
            SequenceProof s2 = new SequenceProof();
            Texte tex2 = new Texte("Quelque soit x un nombre réel");
            Equal eqEx1 = new Equal(new UnknownTerm("y"), new Sum(new Multiplication(new Coefficient("a"), new Power(new UnknownTerm("x"), new NumericValue(2.0d))),
                                                                  new Multiplication(new Coefficient("b"), new UnknownTerm("x")),
                                                                  new Coefficient("c")));
            eqEx1 = eqEx1.MakeUnique() as Equal;
            s2.Add(tex2);
            s2.Add(eqEx1);

            SequenceProof s3 = new SequenceProof();
            Texte tex3 = new Texte("Quelque soit x un nombre réel");
            Equal eqEx3 = new Equal(new Division(new UnknownTerm("y"),
                new Coefficient("a")), new Sum(new Power(new UnknownTerm("x"), new NumericValue(2.0d)),
                new Multiplication(new Division(new Coefficient("b"), new Coefficient("a")), new UnknownTerm("x")),
                new Division(new Coefficient("c"), new Coefficient("a"))));
            s3.Add(tex3, eqEx3);

            SequenceProof s4 = new SequenceProof();
            Texte tex4 = new Texte("Quelque soit x un nombre réel");
            Equal eqEx4 = new Equal(new Division(new UnknownTerm("y"),
                new Coefficient("a")), new Sum(new Product(new UnknownTerm("x"),
                    new Addition(new UnknownTerm("x"), new Division(new Coefficient("b"), new Coefficient("a")))),
                new Division(new Coefficient("c"), new Coefficient("a"))));
            s4.Add(tex4, eqEx4);

            SequenceProof s5 = new SequenceProof();
            Texte tex5 = new Texte("Forme algébrique somme-produit");
            Equal eqEx5 = new Equal(new Division(new Soustraction(new UnknownTerm("y"), new Coefficient("c")),
                new Coefficient("a")), new Product(new UnknownTerm("x"),
                    new Addition(new UnknownTerm("x"), new Division(new Coefficient("b"), new Coefficient("a")))));
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
            Wording w = new Wording("Résolution du polynôme d'ordre 2", "Considérons la somme de deux carrés");

            Texte t1 = new Texte("Quelque soit u et v, deux nombres réels");
            Sum eq1 = new Sum(new Power(new UnknownTerm("u"), new NumericValue(2.0d)), 
                                        new Power(new UnknownTerm("v"), new NumericValue(2.0d)));
            SequenceProof sp1 = new SequenceProof(t1, eq1);

            Texte t2 = new Texte("Quelque soit u et v, deux nombres réels");
            Equal eq2 = new Equal(eq1, new Soustraction(new Power(new Addition(new UnknownTerm("u"), new UnknownTerm("v")), new NumericValue(2.0d)),
                                                        new Product(new NumericValue(2.0d), new UnknownTerm("u"), new UnknownTerm("v"))));
            SequenceProof sp2 = new SequenceProof(t2, eq2);

            Equal eq3 = new Equal(new Sum(new Power(new UnknownTerm("u"), new NumericValue(2.0d)), new Product(new NumericValue(2.0d), new UnknownTerm("u"), new UnknownTerm("v"))),
                                  new Soustraction(new Power(new Addition(new UnknownTerm("u"), new UnknownTerm("v")), new NumericValue(2.0d)), new Power(new UnknownTerm("v"), new NumericValue(2.0d))));

            Texte t3 = new Texte("Puis, appliquer la résolution du polynôme d'ordre 2 dans le cas d'une différence de deux carrés");

            SequenceProof sp3 = new SequenceProof(eq3, t3);

            Texte t4 = new Texte("Quelque soit x0 et x");
            Equal eq4u = new Equal(new UnknownTerm("u"), new UnknownTerm("x"));
            Equal eq4v = new Equal(new UnknownTerm("v"), new UnknownTerm("x_0"));

            Equal eq41 = new Equal(new Sum(new Power(new UnknownTerm("x"), new NumericValue(2.0d)), new Product(new NumericValue(2.0d), new UnknownTerm("x"), new UnknownTerm("x_0"))),
                                  new Soustraction(new Power(new Addition(new UnknownTerm("x"), new UnknownTerm("x_0")), new NumericValue(2.0d)), new Power(new UnknownTerm("x_0"), new NumericValue(2.0d))));

            SequenceProof sp4 = new SequenceProof(t4, eq4u, eq4v, eq41);

            Answer a1 = new Answer("Somme de deux carrés", sp1);
            Answer a2 = new Answer("Identité remarquable", sp2);
            Answer a3 = new Answer("Calcul du plus proche carré d'un nombre", sp3);
            Answer a4 = new Answer("Expression de x0 et x", sp4);

            Exercice e1 = new Exercice(1, "Poser la somme de deux carrés", "Choisissez les lettres u et v", a1);
            Exercice e2 = new Exercice(2, "Utilisez l'identité remarquable connue pour exprimer la somme de deux carrés", "Utilisez la formule du binôme de Newton à l'ordre 2", a2);
            Exercice e3 = new Exercice(3, "Rapporter une différence en inversant les termes", "Cela permet de trouver le plus proche carré d'un nombre", a3);
            Exercice e4 = new Exercice(4, "Retrouver l'équation du produit égale au produit d'un polynôme d'ordre 2", "Poser x0 et x", a4);
            w.Add(e1);
            w.Add(e2);
            w.Add(e3);
            w.Add(e4);

            FlowDocument fd = new FlowDocument();
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
            w.ToDocument(fd);
            return fd;
        }

    }
}