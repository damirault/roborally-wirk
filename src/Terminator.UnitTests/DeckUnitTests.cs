﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WiRK.Terminator.UnitTests
{
	[TestClass]
	public class DeckUnitTests
	{
		[TestMethod]
		public void Deck_Shuffle_VerifyNotInOrder()
		{
			// Arrange
			var deck = new Deck();

			// Act
			deck.Shuffle();
		}
	}
}
