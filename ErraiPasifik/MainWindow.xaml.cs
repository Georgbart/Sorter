using System;
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
using System.Threading;

namespace ErraiPasifik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Change GCF value Here
        const int valueGCF = 20;

        public MainWindow()
        {
            InitializeComponent();
        }

        private class items
        {
            public int numbers { get; set; }

        }

        private void ButtonSort_Click(object sender, RoutedEventArgs e)
        {
            string[] splittedString;    //Input String Splitted
            int[] nums; //SplittedString converted to int[]
            splittedString = TextboxInput.Text.Trim().Split(',');
            nums = Array.ConvertAll(splittedString, int.Parse);
            //Quick Sort
            //Thread t0 = new Thread(() => QuickSort_Thread(nums));
            //Thread t1 = new Thread(() => BubleSort_Thread(nums));
            //Thread t2 = new Thread(() => MergeSort_Thread(nums));
            //Thread t3 = new Thread(() => GCFBubleSort_Thread(nums));

            //t0.Start();
            //t1.Start();
            //t2.Start();
            //t0.Start();

            QuickSort_Thread(nums);
            BubleSort_Thread(nums);
            MergeSort_Thread(nums);
            GCFBubleSort_Thread(nums);
        }



        void Swap(int[] inputs, int a, int b)
        {
            int temp = inputs[a];
            inputs[a] = inputs[b];
            inputs[b] = temp;
        }

        int Partition(int[] inputs, int startIndex, int endIndex)
        {
            int pivot = inputs[endIndex];
            int pointer = startIndex - 1;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (inputs[i] <= pivot)
                {
                    pointer++;
                    Swap(inputs, pointer, i);
                }
            }
            Swap(inputs, pointer+1, endIndex);
            return pointer+1;
        }

        private void QuickSort(int[] inputs, int first, int last)
        {
            int pivot;
            if(first < last)
            {
                pivot = Partition(inputs, first, last);
                QuickSort(inputs, first, pivot-1);
                QuickSort(inputs, pivot+1, last);
            }
        }

        private void QuickSort_Thread(int[] numsIn)
        {
            var watch = new System.Diagnostics.Stopwatch();

            //Clone inputs
            int[] nums = (int[])numsIn.Clone();

            watch.Start();
            QuickSort(nums, 0, nums.Length - 1);
            watch.Stop();
            TextboxQS.Text = String.Join(",", nums);

            List<items> rows = new List<items>();
            for (int i = 0; i < nums.Length; i++)
            {
                rows.Add(new items{ numbers = nums[i] });
            }
            GridQS.ItemsSource = rows;

            LabelQS.Content = watch.ElapsedMilliseconds.ToString() + " ms";
        }

        private void BubleSort(int[] inputs)
        {
            int length = inputs.Length;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    if(inputs[j] > inputs[j+1])
                    {
                        Swap(inputs, j, j+1);
                    }
                }

            }
        }

        private void BubleSort_Thread(int[] numsIn)
        {
            var watch = new System.Diagnostics.Stopwatch();

            //Clone inputs
            int[] nums = (int[])numsIn.Clone();

            watch.Start();
            BubleSort(nums);
            watch.Stop();
            TextboxBS.Text = String.Join(",", nums);

            List<items> rows = new List<items>();
            for (int i = 0; i < nums.Length; i++)
            {
                rows.Add(new items { numbers = nums[i] });
            }
            GridBS.ItemsSource = rows;

            LabelBS.Content = watch.ElapsedMilliseconds.ToString() + " ms";
        }

        private void MergeSort(int[] inputs)
        {
            int mid;    //Middle Index
            int[] arrayL;
            int[] arrayR;
            int i, j, z;
            i = 0;
            j = 0;
            z = 0;

            if (inputs.Length > 1)
            {
                mid = inputs.Length / 2;
                arrayL = inputs.Take(mid).ToArray();
                arrayR = inputs.Skip(mid).ToArray();

                MergeSort(arrayL);
                MergeSort(arrayR);

                while ((i < arrayL.Length) & (j < arrayR.Length))
                {
                    if(arrayL[i] < arrayR[j])
                    {
                        inputs[z] = arrayL[i];
                        i++;
                    }else
                    {
                        inputs[z] = arrayR[j];
                        j++;
                    }
                    z++;
                }

                while (i < arrayL.Length)
                {
                    inputs[z] = arrayL[i];
                    i++;
                    z++;
                }

                while (j < arrayR.Length)
                {
                    inputs[z] = arrayR[j];
                    j++;
                    z++;
                }
            }
        }

        private void MergeSort_Thread(int[] numsIn)
        {
            var watch = new System.Diagnostics.Stopwatch();

            //Clone inputs
            int[] nums = (int[])numsIn.Clone();

            watch.Start();
            MergeSort(nums);
            watch.Stop();
            TextboxMS.Text = String.Join(",", nums);

            List<items> rows = new List<items>();
            for (int i = 0; i < nums.Length; i++)
            {
                rows.Add(new items { numbers = nums[i] });
            }
            GridMS.ItemsSource = rows;

            LabelMS.Content = watch.ElapsedMilliseconds.ToString() + " ms";
        }

        static int GCF(int a, int b)
        {
            int retVal = a;
            int otherval = b;
            int Remainder;

            while (otherval != 0)
            {
                Remainder = retVal % otherval;
                retVal = otherval;
                otherval = Remainder;
            }
            return retVal;
        }

        private void GCFBubleSort(int[] GCFinputs, int[] inputs)
        {
            int length = inputs.Length;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    if (GCFinputs[j] > GCFinputs[j + 1])
                    {
                        Swap(GCFinputs, j, j + 1);
                        Swap(inputs, j, j + 1);
                    }
                    else if (GCFinputs[j] == GCFinputs[j + 1])
                    {
                        if (inputs[j] > inputs[j + 1])
                        {
                            Swap(GCFinputs, j, j + 1);
                            Swap(inputs, j, j + 1);
                        }
                    }
                }

            }
        }

        private void GCFBubleSort_Thread(int[] numsIn)
        {
            var watch = new System.Diagnostics.Stopwatch();

            //Clone inputs
            int[] nums = (int[]) numsIn.Clone();
            int[] numsGCF = (int[]) numsIn.Clone();

            for (int i = 0; i < numsGCF.Length; i++)
            {
                numsGCF[i] = GCF(valueGCF, nums[i]);
            }

            watch.Start();
            GCFBubleSort(numsGCF, nums);
            watch.Stop();
            //TextboxMS.Text = String.Join(",", nums);

            List<items> rows = new List<items>();
            for (int i = 0; i < nums.Length; i++)
            {
                //rows.Add(new items { numbers = numsGCF[i] });
                rows.Add(new items { numbers = nums[i] });
            }
            GridGCFS.ItemsSource = rows;

            LabelGCFS.Content = watch.ElapsedMilliseconds.ToString() + " ms";
        }

    }
}
