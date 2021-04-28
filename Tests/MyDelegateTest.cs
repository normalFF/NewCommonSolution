using System;
using System.Diagnostics;
using NUnit.Framework;
using ClassLibrary.OtherObjects;

namespace Tests
{
	[TestFixture]
	class MyDelegateTest
	{
		delegate void MethodVoid();
		delegate int MethodInt(int value);
		MyDelegate myDelegateVoid;
		MyDelegate myDelegateInt;

		[SetUp]
		public void Setup()
		{
			MethodVoid methodVoid = PrintHello;
			MethodInt methodInt = Inc;
			myDelegateVoid = new(methodVoid.Method);
			myDelegateInt = new(methodInt.Method);
		}

		[Test]
		public void TestMyDelegate()
		{
			MethodVoid methodVoid = PrintHello;
			myDelegateVoid += methodVoid.Method;
			methodVoid -= PrintHello;
			methodVoid += PrintHi;
			myDelegateVoid += methodVoid.Method;
			myDelegateVoid += methodVoid.Method;
			methodVoid -= PrintHi;
			methodVoid += ThrowException;
			myDelegateVoid += methodVoid.Method;
			myDelegateVoid += methodVoid.Method;
			myDelegateVoid.Invoke(this, null);
		}

		[Test]
		public void TestMyDelegateReturnValue()
		{
			MethodInt method = Inc;
			myDelegateInt += method.Method;
			method -= Inc;
			method += Dec;
			myDelegateInt += method.Method;
			myDelegateInt += method.Method;
			method -= Dec;
			method += ThrowException;
			myDelegateInt += method.Method;
			myDelegateInt += method.Method;
			var value = (int)myDelegateInt.Invoke(this, new object[] { 6 });
			Console.WriteLine($"Result: {value}");
		}

		[Test]
		public void TestEqualsMyDelegate()
		{
			Assert.IsTrue(!myDelegateInt.Equals(myDelegateVoid));
		}

		private void PrintHello()
		{
			Console.WriteLine("Hello");
		}

		private void PrintHi()
		{
			Console.WriteLine("Hi");
		}

		private void ThrowException()
		{
			throw new NotImplementedException();
		}

		private int Inc(int value)
		{
			return value += 2;
		}

		private int Dec(int value)
		{
			return value -= 2;
		}

		private int ThrowException(int value)
		{
			throw new DivideByZeroException();
		}
	}
}
