using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;
using SharpTestsEx;

namespace GoogleFeed.Tests
{    
    public class given_a_request
    {
        [TestFixture]
        public class when_there_is_not_a_cached_file
        {
            FileProvider _provider;
            string cacheDir = @"C:\temp";
            string cacheFileName = "google_products.xml";
            string fileName = String.Empty;
            
            [SetUp]
            public void setup()
            {
                fileName = Path.Combine(cacheDir, cacheFileName);

                if (File.Exists(fileName))
                    File.Delete(fileName);

                _provider = new FileProvider(cacheDir, cacheFileName);
            }
            
            [Test]
            public void should_return_a_new_stream()
            {
                bool isFileNew = true;

                using (var writer = _provider.GetWriter(out isFileNew))
                {
                    writer.Should().Not.Be.Null();
                    writer.Should().Be.OfType<FileStream>();
                    isFileNew.Should().Be.True();

                    writer.Close();
                }
            }

            [TearDown]
            public void TearDown()
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
        }

        [TestFixture]
        public class when_there_is_a_filled_file_ready
        {
            FileProvider _provider;
            string cacheDir = @"C:\temp";
            string cacheFileName = "google_products.xml";
            string fileName = String.Empty;

            [SetUp]
            public void setup()
            {
                fileName = Path.Combine(cacheDir, cacheFileName);

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);                    
                }

                using (var f = File.Create(fileName)) { f.Close(); }

                _provider = new FileProvider(cacheDir, cacheFileName);
            }

            [Test]
            public void should_return_it()
            {
                bool isFileNew = true;

                using (var writer = _provider.GetWriter(out isFileNew))
                {
                    writer.Should().Not.Be.Null();
                    writer.Should().Be.OfType<FileStream>();
                    isFileNew.Should().Be.False();

                    writer.Close();
                }
            }

            [TearDown]
            public void TearDown()
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
        }

        [TestFixture]
        public class when_there_is_not_a_filled_file_ready
        {
            ContentProvider _contentProvider;
                        
            [SetUp]
            public void setup()
            {
                bool isFileNew = true;
                var fileProvider = new Mock<IFileProvider>();
                fileProvider.Setup(x => x.GetWriter(out isFileNew))
                        .Returns(new MemoryStream());

                var productsSample = SampleProductsGenerator.Generate(10);
                Func<IEnumerable<Dictionary<string, string>>> content = () => new SampleProductToDictionaryTransformer().Transform(productsSample);

                _contentProvider = new ContentProvider(fileProvider.Object, content);
            }
            
            [Test]
            public void should_fill()
            {
                using (var filledFile = _contentProvider.GetFilledFile())
                {
                    filledFile.Should().Not.Be.Null();
                    
                    filledFile.Close();
                }
            }
        }
    }
}
