using Moq;
using Road23.WebApi.Tests.Mocks;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Road23.WebApi.Tests.RepositoryTests
{
	public class CandleItemTests
	{

		[Fact] 
		public void GetCandles_ReturnsCandlesList()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			var result = candlerepo.GetCandles();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(3, result.Count);
			Assert.IsAssignableFrom<IEnumerable<CandleItem>>(result);
		}

		[Fact]
		public void GetCandleByCorrectId_ReturnsCandle()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			int correctId = 2;
			var result = candlerepo.GetCandleById(correctId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(typeof(CandleItem), result.GetType());
		}

		[Fact]
		public void GetCandleByWrongId_ReturnsNull()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			int wrongId = 0;
			var result = candlerepo.GetCandleById(wrongId);

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public void GetCandleByCorrectName_ReturnsCandle()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			string correctName = "first candle";
			var result = candlerepo.GetCandleByName(correctName);

			// Assert
			Assert.NotNull(result);
			Assert.IsType<CandleItem>(result);	
		}

		[Fact]
		public void CandleExistsByCorrectId_ReturnsTrue()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			int correctId = 1;
			var result = candlerepo.CandleExistsById(correctId);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void GetCandlesFromCorrectCategory_ReturnsCandlesList()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			int correctCategoryId = 1;
			var result = candlerepo.GetCandlesFromCategory(correctCategoryId);

			// Assert
			Assert.NotNull(result);
			Assert.IsAssignableFrom<IEnumerable<CandleItem>>(result);
		}

		[Fact]
		public async void CreateCandle_ReturnsCandleCreated()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			var newCandle = new CandleItem
			{
				Id = 4,
				CategoryId = 4,
				Name = "fourth candle",
				Description = "fourth description",
				RealCost = 4,
				SellPrice = 4,
				BurningTimeMins = 4,
				HeightCM = 4,
				PhotoLink = "",
				Ingredient = new CandleIngredient { Id = 4, CandleId = 4, WaxNeededGram = 4, WickForDiameterCD = 4 },
			};
			var result = await candlerepo.CreateCandleAsync(newCandle);

			// Assert
			Assert.NotNull(result);
			Assert.IsType<CandleItem>(result);
			Assert.Equal(newCandle, result);
		}

		[Fact]
		public async void UpdateCandleItem_ReturnsCandleUpdated()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			var removeCandle = new CandleItem
			{
				Id = 2,
				CategoryId = 2,
				Name = "second candle",
				Description = "second description",
				RealCost = 2,
				SellPrice = 2,
				BurningTimeMins = 2,
				HeightCM = 2,
				PhotoLink = "",
				Ingredient = new CandleIngredient { Id = 2, CandleId = 2, WaxNeededGram = 2, WickForDiameterCD = 2 },
			};
			var result = await candlerepo.RemoveCandleAsync(removeCandle);

			// Assert
			Assert.NotNull(result);
			Assert.IsType<CandleItem>(result);
			Assert.Equal(removeCandle, result);
		}

		[Fact]
		public async void RemoveCandleItem_ReturnsCandleRemoved()
		{
			// Arrange
			var candlerepo = MockRepositoryWrapper.GetCandleMock().Object;

			// Act
			var updateCandle = new CandleItem
			{
				Id = 2,
				CategoryId = 2,
				Name = "second candle",
				Description = "second second",
				RealCost = 2,
				SellPrice = 2,
				BurningTimeMins = 2,
				HeightCM = 2,
				PhotoLink = "",
				Ingredient = new CandleIngredient { Id = 2, CandleId = 2, WaxNeededGram = 2, WickForDiameterCD = 2 },
			};
			var result = await candlerepo.UpdateCandleAsync(updateCandle);

			// Assert
			Assert.NotNull(result);
			Assert.IsType<CandleItem>(result);
			Assert.Equal(updateCandle, result);
		}
	}
}
