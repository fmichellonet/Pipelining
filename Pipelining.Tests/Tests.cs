using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Moq;

namespace Pipelining.Tests
{
    public class Tests
    {
        [Test]
        public void SimpleSourceToDestinationPipe()
        {
            var dummyConsumer = new Mock<IConsumer<string>>();

            var res = new EnumerableSource<string>(new[] { "a", "aa" })
                .Pipe(dummyConsumer.Object)
                .Transform();

            Assert.That(res.ToList(), Is.EquivalentTo(new[] { "a", "aa" }));
            dummyConsumer.Verify(c => c.Consume(It.IsAny<IEnumerable<string>>()), Times.Exactly(1));
        }

        [Test]
        public void OneTransformationPipe()
        {
            var letterCount = new Mock<LetterCount> { CallBase = true };
            var res = new EnumerableSource<string>(new[] { "a", "aa" })
                .Pipe(letterCount.Object)
                .Transform();

            Assert.That(res.ToList(), Is.EquivalentTo(new[] { 1, 2 }));
            letterCount.Verify(c => c.Transform(It.IsAny<IEnumerable<string>>()), Times.Exactly(1));
        }

        [Test]
        public void TerminatedPipeIsStartable()
        {
            var nullConsumer = new Mock<IConsumer<int>>();

            var src = new EnumerableSource<string>(new[] { "a", "aa" });
            src.Pipe(new LetterCount())
                .Pipe(nullConsumer.Object)
                .Start();

            nullConsumer.Verify(c => c.Consume(It.IsAny<IEnumerable<int>>()), Times.Exactly(1));
        }

        [Test]
        public void InlineTransformation()
        {
            var res = new EnumerableSource<string>(new[] { "a", "aa" })
                .Pipe(new Transformer<string, int>(element => element.Length))
                .Transform();

            Assert.That(res.ToList(), Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test]
        public void InlineGenerator()
        {
            string[] src = { "a", "aa" };
            var enu = src.GetEnumerator();

            var res = new Generator<string>(() =>
            {
                if (enu.MoveNext())
                    return (string)enu.Current;
                return null;
            })
                .Pipe(new LetterCount())
                .Transform();

            Assert.That(res.ToList(), Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test]
        public void InlineConsumer()
        {
            var counter = 0;

            new EnumerableSource<string>(new[] { "a", "aa" })
                .Pipe(new Consumer<string>(i => { counter++; }))
                .Start();

            Assert.That(counter, Is.EqualTo(2));
        }

        [Test]
        public void InlinePipeline()
        {
            string[] src = { "a", "aa" };
            var enu = src.GetEnumerator();
            var counter = 0;

            new Generator<string>(() =>
            {
                if (enu.MoveNext())
                    return (string)enu.Current;
                return null;
            })
                .Pipe(new Transformer<string, int>(element => element.Length))
                .Pipe(new Consumer<int>(i => { counter++; }))
                .Start();

            Assert.That(counter, Is.EqualTo(2));
        }

        [Test]
        public void WordCountMapReduce()
        {
            IEnumerable<int> res = new EnumerableSource<string>(new[] { "Hello world", "How are you?" })
                .Pipe(new Transformer<string, string>(sentence =>
                {
                    return Regex.Split(sentence, @"\W")
                        .Where(w => w != String.Empty);
                }))
                .Pipe(new Transformer<string, int>(word => 1))
                .Transform();

            List<int> t = res.ToList();
        }
    }
}