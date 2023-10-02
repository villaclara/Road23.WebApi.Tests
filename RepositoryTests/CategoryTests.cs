using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Road23.WebApi.Tests.Mocks;
using Road23.WebAPI.Controllers;
using Road23.WebAPI.Database;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Road23.WebApi.Tests.RepositoryTests
{
	public class CategoryTests
	{
		private async Task<ApplicationContext> GetAppContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
			var appContext = new ApplicationContext(options);
			appContext.Database.EnsureCreated();

			if (!await appContext.CandleCategories.AnyAsync())
			{
				await appContext.CandleCategories.AddRangeAsync(
					new[]
					{
						new CandleCategory
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
						new CandleCategory
						{
							Id = 2,
							Name = "second category",
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
						},
						new CandleCategory
						{
							Id = 3,
							Name = "third category",
							Candles = new List<CandleItem>()
						}
					});

				await appContext.SaveChangesAsync();
			}

			return appContext;
		}


		[Fact]
		public async void GetAllCategories_ReturnsAllCategories()
		{
			// Arrange
			var categoryRepositoryEF = new CandleCategoryRepository(await GetAppContext());

			// Act
			var result = categoryRepositoryEF.GetCategories();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(3, result.Count);
			Assert.IsAssignableFrom<IEnumerable<CandleCategory>>(result);

		}


		[Fact]
		public async void GetCategoryByWrongID_ReturnsDefaultCategory()
		{
			// Arrange
			var categoryRepositoryEF = new CandleCategoryRepository(await GetAppContext());

			// Act
			int wrongID = 10;
			var result = categoryRepositoryEF.GetCategoryById(wrongID);

			// Assert
			Assert.Null(result);
			Assert.True(result is default(CandleCategory));
		}

		[Fact]
		public async void GetCategoryByCorrectName_ReturnsCategory()
		{
			// Arrange
			var categoryRepositoryEF = new CandleCategoryRepository(await GetAppContext());

			// Act
			string correctName = "second category";
			var result = categoryRepositoryEF.GetCategoryByName(correctName);

			// Assert
			Assert.NotNull(result);
			Assert.IsAssignableFrom<CandleCategory>(result);
		}

		[Fact]
		public async void CandlesExistInCategoryID_ReturnsTrue()
		{
			// Arrange
			var categoryRepositoryEF = new CandleCategoryRepository(await GetAppContext());

			// Act
			int correctId = 1;
			var result = categoryRepositoryEF.CandlesExistInCategoryId(correctId);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public async void CandlesNOTExistInCategoryID_ReturnsFalse()
		{
			// Arrange
			var categoryRepositoryEF = new CandleCategoryRepository(await GetAppContext());

			// Act
			int correctId = 3;
			var result = categoryRepositoryEF.CandlesExistInCategoryId(correctId);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public async void AddCategory_ReturnsCategory()
		{
			// Arrange
			var categoryRepositoryEF = new CandleCategoryRepository(await GetAppContext());

			// Act
			var newcategory = new CandleCategory
			{
				Id = 4,
				Name = "fourth category",
				Candles = null
			};
			var result = await categoryRepositoryEF.CreateCategoryAsync(newcategory);

			// Assert
			Assert.NotNull(result);
			Assert.IsAssignableFrom<CandleCategory>(result);
		}
	}
}
