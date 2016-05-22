using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _120202103_1
{
    class Program
    {
        static void Main(string[] args)
        {
            HiddenMarkov hmm = new HiddenMarkov();
            Console.ReadLine();
        }
        public class Match
        {

            private int matchNum;
            private char uniqueChar;
            private double probability;

            public virtual string getMatch()
            {
                return match;
            }

            public virtual void setMatch(string match)
            {
                this.match = match;
            }

            private string match;

            public virtual bool Protected
            {
                get
                {
                    return isProtected;
                }
            }

            public virtual bool IsProtected
            {
                set
                {
                    this.isProtected = value;
                }
            }

            private bool isProtected;


            public Match(int matchNum, char uniqueChar, double probability, string match)
            {
                this.matchNum = matchNum;
                this.uniqueChar = uniqueChar;
                this.probability = probability;
                this.match = match;
            }


            public Match(bool isProtected)
            {
                this.isProtected = isProtected;
            }

            public virtual int MatchNum
            {
                get
                {
                    return matchNum;
                }
                set
                {
                    this.matchNum = value;
                }
            }


            public virtual char UniqueChar
            {
                get
                {
                    return uniqueChar;
                }
                set
                {
                    this.uniqueChar = value;
                }
            }


            public virtual double Probability
            {
                get
                {
                    return probability;
                }
                set
                {
                    this.probability = value;
                }
            }


            public virtual double Transaction
            {
                get
                {
                    return transaction;
                }
                set
                {
                    this.transaction = value;
                }
            }


            public Match(double transaction, int transactionIndex)
            {
                this.transaction = transaction;
                this.transactionIndex = transactionIndex;
            }

            public virtual int TransactionIndex
            {
                get
                {
                    return transactionIndex;
                }
                set
                {
                    this.transactionIndex = value;
                }
            }


            private int transactionIndex;
            private double transaction;
        }
        public class Delete
        {

            private double transactionToDelete;

            public virtual int DeleteNum
            {
                get
                {
                    return deleteNum;
                }
                set
                {
                    this.deleteNum = value;
                }
            }



            public virtual double TransactionToDelete
            {
                get
                {
                    return transactionToDelete;
                }
                set
                {
                    this.transactionToDelete = value;
                }
            }


            private int deleteNum;


            public Delete(double transactionToDelete, int deleteNum)
            {
                this.transactionToDelete = transactionToDelete;
                this.deleteNum = deleteNum;
            }
        }

        public class Insert
        {

            public Insert(double transactionToInsert, int insertNum)
            {
                this.transactionToInsert = transactionToInsert;
                this.insertNum = insertNum;
            }

            public virtual double TransactionToInsert
            {
                get
                {
                    return transactionToInsert;
                }
            }

            public virtual double TransactionToDelete
            {
                set
                {
                    this.transactionToInsert = value;
                }
            }

            public virtual int InsertNum
            {
                get
                {
                    return insertNum;
                }
                set
                {
                    this.insertNum = value;
                }
            }


            private double transactionToInsert;
            private int insertNum;
        }
        public class HiddenMarkov
        {


            internal List<char?> differentkars = new List<char?>(); 
            internal StringBuilder sequences; 
            internal int sutun_lgn;
            internal int satirlarin_Sayi;
            internal IList<string> linesForColumn = new List<string>();

            internal LinkedList<Delete> Delete_Listesi = new LinkedList<Delete>();
            internal LinkedList<Insert> İnsert_Listesi = new LinkedList<Insert>();

            internal LinkedList<Match> Match_İslem = new LinkedList<Match>();

            internal int instertnodesy;
            internal int silinennode_;

            internal LinkedList<Match> matchlerinlist = new LinkedList<Match>();

            internal LinkedList<Match> Korumali_mi = new LinkedList<Match>();

            internal char kar;

            internal int counter = 0;
            internal double sayy = 0;
            internal double skor = 0;

            public HiddenMarkov()
            {

                collectkars();

                IEnumerator sayacc = differentkars.GetEnumerator();

           

              

                findProbabilty();


                Console.WriteLine("Korumalı mı Korumasız mı Bakalım\n ");
                int i = 0;
                foreach (Match p in Korumali_mi)
                {
                    Console.Write("Match " + (i + 1) + " " + p.Protected+" ");
                    i++;
                }

              
                Console.WriteLine("\nHarf Harf Matchlerın Olasılıkları\n");
                foreach (Match m in matchlerinlist)
                {
                    Console.Write("Match " + m.MatchNum + " " + m.UniqueChar + " Oranımız:" + m.Probability + " "+ "\t");
                }
                Console.WriteLine("\n");
                silinennode_ = sutun_lgn;
                instertnodesy = sutun_lgn + 1;


                transactions();
                Console.WriteLine("numaralar ile her durum için olasalıklara bakıyoruz");
                foreach (Delete d in Delete_Listesi)
                {
                    Console.WriteLine("Delete." + d.DeleteNum + " teki durum " + d.TransactionToDelete+"\t");
                }

                foreach (Match m in Match_İslem)
                {
                    Console.WriteLine("Match." + m.TransactionIndex + " teki durum " + m.Transaction+"\t");
                }

                foreach (Insert @in in İnsert_Listesi)
                {
                    Console.WriteLine("Insert." + @in.InsertNum + " teki durum " + @in.TransactionToInsert+"\t");
                }
                Console.WriteLine("\n");




            }



            public virtual void collectkars()
            {

                Dosyaokumaca();

                for (int i = 0; i < sequences.Length; i++)
                {
                    if (!differentkars.Contains(sequences[i]))
                    {
                        if (sequences[i] != '\n')
                        {
                            differentkars.Add(sequences[i]);
                        }
                    }
                }
       


                for (int i = 0; i < sutun_lgn; i++)
                {
                    StringBuilder columnSequence = new StringBuilder();
                    for (int j = 0; j < satirlarin_Sayi; j++)
                    {
                        string[] a = sequences.ToString().Split('\n','t');

                        try
                        {

                            columnSequence.Append(a[j][i]);


                        }
                        catch (Exception)
                        {

                        }
                    }
                    linesForColumn.Add(columnSequence.ToString());
                  


                }


            }

            public virtual void findProbabilty()
            {
                for (int i = 0; i < sutun_lgn; i++)
                {
                    foreach (char? c in differentkars)
                    {
                        counter = 0;
                        for (int j = 0; j < satirlarin_Sayi; j++)
                        {
                            if (c == linesForColumn[i][j])
                            {
                                counter++;
                            }
                        }

                        matchlerinlist.AddLast(new Match(i + 1, (char)c, counter / (double)satirlarin_Sayi, linesForColumn[i]));
                        if (c != '-')
                        {
                            sayy += counter / (double)satirlarin_Sayi;
                        }
                        else
                        {
                            skor = counter / (double)satirlarin_Sayi;
                        }
                    }

                    if (skor >= sayy)
                    {
                        Korumali_mi.AddLast(new Match(false));
                    }
                    else
                    {
                        Korumali_mi.AddLast(new Match(true));
                    }

                    sayy = 0;


                }
            }


            public virtual void Dosyaokumaca()
            {

                sequences = new StringBuilder();
                System.IO.StreamReader oku = null;

                try
                {
                    oku = new System.IO.StreamReader(@"C:\dna.txt");
                }
                catch (System.IO.FileNotFoundException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                System.IO.StreamReader oku2 = new System.IO.StreamReader(@"C:\dna.txt");

                string satir_içerik = null;
                satirlarin_Sayi = 0;
                try
                {
                    while ((satir_içerik = oku2.ReadLine()) != null)
                    {
                        sequences.Append(satir_içerik + "\n");
                        sutun_lgn = satir_içerik.Length;
                        satirlarin_Sayi++;
                    }

                }
                catch (IOException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }

                // System.out.println(sequences);
                try
                {
                    oku.Close();
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }

            }



            public virtual void transactions()
            {
                int counterDeleteOfProtected = 0, counterMatchOfProtected = 0, counterInsertOfProtected = 0;
                int[] indexDelete = new int[satirlarin_Sayi];
                int[] indexMatch = new int[satirlarin_Sayi];
                int[] indexInsert = new int[satirlarin_Sayi];

                double[] deleteTransaction = new double[sutun_lgn];
                double[] matchTransaction = new double[sutun_lgn];
                double[] insertTransaction = new double[sutun_lgn];
                for (int i = 0; i < sutun_lgn; i++)
                {
                    counterDeleteOfProtected = 0;
                    counterMatchOfProtected = 0;
                    counterInsertOfProtected = 0;
                    if (Korumali_mi.ElementAt(i).Protected== true)
                    {
                        for (int j = 0; j < satirlarin_Sayi; j++)
                        {
                            if (linesForColumn[i][j] == '-')
                            {
                                counterDeleteOfProtected++;
                                indexDelete[j] = j + 1;
                            }
                            else
                            {
                                counterMatchOfProtected++;
                                indexMatch[j] = j + 1;
                            }
                        }
                        deleteTransaction[i] = counterDeleteOfProtected / (double)satirlarin_Sayi;
                        matchTransaction[i] = counterMatchOfProtected / (double)satirlarin_Sayi;
                        //System.out.println(" "+counterMatchOfProtected/(double)satirlarin_Sayi+" : "+counterDeleteOfProtected/(double)satirlarin_Sayi);
                    }
                    else
                    {
                        for (int j = 0; j < satirlarin_Sayi; j++)
                        {
                            if (linesForColumn[i][j] != '-')
                            {
                                counterInsertOfProtected++;
                                indexInsert[j] = j + 1;
                            }
                        }
                        insertTransaction[i] = counterInsertOfProtected / (double)satirlarin_Sayi;

                    }
                }


                for (int j = 0; j < satirlarin_Sayi; j++)
                {
                    if (deleteTransaction[j] != 0.0)
                    {
                        Delete_Listesi.AddLast(new Delete(deleteTransaction[j], indexDelete[j]));
                    }
                    if (matchTransaction[j] != 0.0)
                    {
                        Match_İslem.AddLast(new Match(matchTransaction[j], indexMatch[j]));
                    }
                    if (insertTransaction[j] != 0.0)
                    {
                        İnsert_Listesi.AddLast(new Insert(insertTransaction[j], indexInsert[j]));
                    }
                }
            }


        }

    }
}
