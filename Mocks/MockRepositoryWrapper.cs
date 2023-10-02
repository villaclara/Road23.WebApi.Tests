using Moq;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Road23.WebApi.Tests.Mocks
{
	public class MockRepositoryWrapper
	{
		public static Mock<ICandleCategoryRepository> GetCategoryMock()
		{
			// new mock item
			var mock = new Mock<ICandleCategoryRepository>();

			// fake database of categories
			var categories = new List<CandleCategory>()
			{
				new CandleCategory()
				{
					Id = 1,
					Name = "first category",
					Candles = new List<CandleItem>()
					{
						new CandleItem()
						{
							Id = 1,
							Name = "first candle",
							Description = "1 desc",
							RealCost = 1,
							SellPrice = 1,
							BurningTimeMins = 1,
							HeightCM = 1,
							Ingredient = new CandleIngredient()
							{
								CandleId = 1,
								Id = 1,
								WaxNeededGram = 1,
								WickForDiameterCD = 1
							},
							PhotoLink = "",
							CategoryId = 1
						}
					}
				},
				new CandleCategory()
				{
					Id = 2,
					Name = "2 category",
					Candles = new List<CandleItem>()
					{
						new CandleItem()
						{
							Id = 2,
							Name = "second candle",
							Description = "2 desc",
							RealCost = 2,
							SellPrice = 2,
							BurningTimeMins = 2,
							HeightCM = 2,
							Ingredient = new CandleIngredient()
							{
								CandleId = 2,
								Id = 2,
								WaxNeededGram = 2,
								WickForDiameterCD = 2
							},
							PhotoLink = "",
							CategoryId = 2
						},
						new CandleItem()
						{
							Id = 3,
							Name = "third candle",
							Description = "3 desc",
							RealCost = 3,
							SellPrice = 3,
							BurningTimeMins = 3,
							HeightCM = 3,
							Ingredient = new CandleIngredient()
							{
								CandleId = 3,
								Id = 3,
								WaxNeededGram = 3,
								WickForDiameterCD = 3
							},
							PhotoLink = "",
							CategoryId = 2
						}
					}
				}
			};


			// setting up all methods from the ICandleCategoryRepository interface


			// GetCategories
			mock.Setup(m => m.GetCategories()).Returns(() => categories);


			// GetCategoryById - 
			// It.IsAny<int> matches the argument type of the method
			mock.Setup(m => m.GetCategoryById(It.IsAny<int>())).Returns((int id) => categories.FirstOrDefault(c => c.Id == id));

			// GetCategoryByName
			mock.Setup(m => m.GetCategoryByName(It.IsAny<string>())).Returns((string name) => categories.FirstOrDefault(c => c.Name == name));

			// CandleExistsInCategoryByID
			mock.Setup(m => m.CandlesExistInCategoryId(It.IsAny<int>())).Returns((int id) => categories.Any(c => c.Id == id && c.Candles is not null));

			// we do not need realization for this 
			// https://code-maze.com/testing-repository-pattern-entity-framework/ 
			mock.Setup(m => m.CreateCategoryAsync(It.IsAny<CandleCategory>())).Callback(() => { return; });

			mock.Setup(m => m.RemoveCategoryAsync(It.IsAny<CandleCategory>())).Callback(() => { return; });

			mock.Setup(m => m.UpdateCategoryAsync(It.IsAny<CandleCategory>())).Callback(() => { return; });


			return mock;
		}

		public static Mock<ICandleItemRepository> GetCandleMock()
		{
			var mock = new Mock<ICandleItemRepository>();

			var candles = new List<CandleItem>()
			{
				new CandleItem
				{
					Id = 1,
					Name = "first candle",
					Description = "first description",
					RealCost = 1,
					SellPrice = 1,
					BurningTimeMins = 1,
					HeightCM = 1,
					PhotoLink = "",
					Ingredient = new CandleIngredient { Id = 1, CandleId = 1, WaxNeededGram = 1, WickForDiameterCD = 1 },
					CategoryId = 1
				},
				new CandleItem
				{
					Id = 2,
					Name = "second candle",
					Description = "second description",
					RealCost = 2,
					SellPrice = 2,
					BurningTimeMins = 2,
					HeightCM = 2,
					PhotoLink = "",
					Ingredient = new CandleIngredient { Id = 2, CandleId = 2, WaxNeededGram = 2, WickForDiameterCD = 2 },
					CategoryId = 1
				},
				new CandleItem
				{
					Id = 3,
					Name = "third candle",
					Description = "third description",
					RealCost = 3,
					SellPrice = 3,
					BurningTimeMins = 3,
					HeightCM = 3,
					PhotoLink = "",
					Ingredient = new CandleIngredient { Id = 3, CandleId = 3, WaxNeededGram = 3, WickForDiameterCD = 3 },
					CategoryId = 1
				},
			};

			mock.Setup(m => m.GetCandles()).Returns(candles);
			mock.Setup(m => m.GetCandleById(It.IsAny<int>())).Returns((int id) => candles.FirstOrDefault(c => c.Id == id));
			mock.Setup(m => m.GetCandleByName(It.IsAny<string>())).Returns((string name) => candles.FirstOrDefault((c) => c.Name == name));
			mock.Setup(m => m.CandleExistsById(It.IsAny<int>())).Returns((int id) => candles.Any(c => c.Id == id));
			mock.Setup(m => m.GetCandlesFromCategory(It.IsAny<int>())).Returns((int id) => candles.Where(c => c.CategoryId == id).ToList());
			mock.Setup(m => m.CreateCandleAsync(It.IsAny<CandleItem>()).Result).Returns((CandleItem item) => item);
			mock.Setup(m => m.UpdateCandleAsync(It.IsAny<CandleItem>()).Result).Returns((CandleItem item) => item);
			mock.Setup(m => m.RemoveCandleAsync(It.IsAny<CandleItem>()).Result).Returns((CandleItem item) => item);

			return mock;
		}
	}
}
