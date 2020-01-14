using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        //[TestMethod()]
        //public void FeedForwardTest()
        //{
        //    var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
        //    var inputs = new int[,]
        //    {
        //        // Результат - Пациент болен - 1
        //        //             Пациент Здоров - 0

        //        // Неправильная температура T
        //        // Хороший возраст A
        //        // Курит S
        //        // Правильно питается F
        //        //T  A  S  F
        //        { 0, 0, 0, 0 },
        //        { 0, 0, 0, 1 },
        //        { 0, 0, 1, 0 },
        //        { 0, 0, 1, 1 },
        //        { 0, 1, 0, 0 },
        //        { 0, 1, 0, 1 },
        //        { 0, 1, 1, 0 },
        //        { 0, 1, 1, 1 },
        //        { 1, 0, 0, 0 },
        //        { 1, 0, 0, 1 },
        //        { 1, 0, 1, 0 },
        //        { 1, 0, 1, 1 },
        //        { 1, 1, 0, 0 },
        //        { 1, 1, 0, 1 },
        //        { 1, 1, 1, 0 },
        //        { 1, 1, 1, 1 }
        //    };

        //    var topology = new Topology(4, 1, 0.1, 2);
        //    var neuralNetwork = new NeuralNetwork(topology);
        //    var difference = neuralNetwork.Learn(outputs, inputs, 10000);

        //    var results = new List<double>();
        //    for(int i = 0; i < outputs.Length; i++)
        //    {
        //        var row = NeuralNetwork.GetRow(inputs, i);
        //        var res = neuralNetwork.Predict(row).Output;
        //        results.Add(res);
        //    }

        //    for(int i = 0; i < results.Count; i++)
        //    {
        //        var expected = Math.Round(outputs[i], 2);
        //        var actual = Math.Round(results[i], 2);
        //        Assert.AreEqual(expected, actual);
        //    }
        //}

        //[TestMethod()]
        //public void RecognizeImages()
        //{
        //    var size = 215;
        //    var parasitizedPath = @"C:\Users\Vika\Desktop\cell_images\Parasitized";
        //    var unparasitizedPath = @"C:\Users\Vika\Desktop\cell_images\Uninfected\";

        //    var converter = new PictureConverter();
        //    var testParasitizedImageInput = converter.Convert(@"C:\Users\Vika\Desktop\neuralnetworks-master (1)\neuralnetworks-master\NeuralNetworksTests\Images\Parasitized.png");
        //    var testUnparasitizedImageInput = converter.Convert(@"C:\Users\Vika\Desktop\neuralnetworks-master (1)\neuralnetworks-master\NeuralNetworksTests\Images\Unparasitized.png");

        //    var topology = new Topology(testParasitizedImageInput.Count, 1, 0.1, testParasitizedImageInput.Count / 2);
        //    var neuralNetwork = new NeuralNetwork(topology);

        //    int[,] parasitizedInputs = GetData(parasitizedPath, converter, testParasitizedImageInput, size);
        //    int[,] unparasitizedInputs = GetData(unparasitizedPath, converter, testParasitizedImageInput, size);

        //    neuralNetwork.Learn(new double[] { 1 }, parasitizedInputs, 3);
        //    neuralNetwork.Learn(new double[] { 0 }, unparasitizedInputs, 3);
            

        //    var par = neuralNetwork.Predict(testParasitizedImageInput.Select(t => (double)t).ToArray());
        //    var unpar = neuralNetwork.Predict(testUnparasitizedImageInput.Select(t => (double)t).ToArray());

        //    var a = Math.Round(par.Output, 2);
        //    var b = Math.Round(unpar.Output, 2);

        //    Assert.AreEqual(1, a);
        //    Assert.AreEqual(0, b);
        //}

        [TestMethod()]
        public void InversionPixelsTest()
        {
            var color1 = Color.FromArgb(49, 208,46);
            var color3 = Color.FromArgb(246, 246,36);
            var color5 = Color.FromArgb(4, 20,71);


            var color2 = Color.FromArgb(124, 73,236);
            var color4 = Color.FromArgb(11, 12,242);
            var color6 = Color.FromArgb(240, 227,182);

            var topology = new Topology(3, 3, 0.1, 10, 10);
            var neuralNetwork = new NeuralNetwork(topology);

            var expected = new List<double[]>
            {
                new[]{ConvertToDouble(color1.R), ConvertToDouble(color1.G), ConvertToDouble(color1.B)},
                new[]{ConvertToDouble(color3.R), ConvertToDouble(color3.G), ConvertToDouble(color3.B)},
                new[]{ConvertToDouble(color5.R), ConvertToDouble(color5.G), ConvertToDouble(color5.B)}
            };

            var inputs = new List<double[]>
            {
                new[]{ConvertToDouble(color2.R), ConvertToDouble(color2.G), ConvertToDouble(color2.B)},
                new[]{ConvertToDouble(color4.R), ConvertToDouble(color4.G), ConvertToDouble(color4.B)},
                new[]{ConvertToDouble(color6.R), ConvertToDouble(color6.G), ConvertToDouble(color6.B)}
            };

            var err1 = neuralNetwork.LearnTest(expected, inputs, 50);

            var testColor = Color.FromArgb(124, 73, 236);

            var a = neuralNetwork.PredictTest(ConvertToDouble(testColor));
            var aa = ConvertToColor(a);
        }

        [TestMethod()]
        public void ChangeImageTest()
        {
            var firstPath = @"C:\Users\Vika\Desktop\neuralnetworks-master (1)\neuralnetworks-master\NeuralNetworksTests\Images\First.png";
            var secondPath = @"C:\Users\Vika\Desktop\neuralnetworks-master (1)\neuralnetworks-master\NeuralNetworksTests\Images\Second.png";
            var testPath = @"C:\Users\Vika\Desktop\neuralnetworks-master (1)\neuralnetworks-master\NeuralNetworksTests\Images\test.png";

            var topology = new Topology(3, 3, 0.1, 5);
            var neuralNetwork = new NeuralNetwork(topology);

            var converter = new PictureConverter();

            var inputs = ConvertToDouble(converter.ConvertToPixels(firstPath));
            var expected = ConvertToDouble(converter.ConvertToPixels(secondPath));


            var err1 = neuralNetwork.LearnTest(expected, inputs, 10);

            var testPixels = ConvertToDouble(converter.ConvertToPixels(firstPath));


            var resultPixels = new List<Color>();
            foreach (var pixel in testPixels)
            {
                resultPixels.Add(ConvertToColor(neuralNetwork.PredictTest(pixel)));
            }

            converter.Save("e:\\FINALimage.png", resultPixels);

        }

        private Color ConvertToColor(List<Neuron> neurons)
        {
            return Color.FromArgb(
                Convert.ToInt32(neurons[0].Output * 255), 
                Convert.ToInt32(neurons[1].Output * 255),
                Convert.ToInt32(neurons[2].Output * 255)
                );
        }

        private static List<double[]> ConvertToDouble(List<Color> pixels)
        {
            var result = new List<double[]>();
            foreach (var pixel in pixels)
            {
                result.Add(ConvertToDouble(pixel));
            }

            return result;
        }

        private static double[] ConvertToDouble(Color color)
        {
            return new[]
            {
                ConvertToDouble(color.R),
                ConvertToDouble(color.G),
                ConvertToDouble(color.B)
            };
        }

        private static double ConvertToDouble(byte color)
        {
            return Math.Round(Convert.ToDouble(color) / 255, 3);
        }

        private static int[,] GetData(string parasitizedPath, PictureConverter converter, List<int> testImageInput, int size)
        {
            var images = Directory.GetFiles(parasitizedPath);
            var result = new int[size, testImageInput.Count];
            for (var i = 0; i < size; i++)
            {
                var image = converter.Convert(images[i]);
                for (var j = 0; j < image.Count; j++)
                {
                    result[i, j] = image[j];
                }
            }

            return result;
        }
    }
}