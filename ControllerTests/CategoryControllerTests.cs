using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Road23.WebApi.Tests.Mocks;
using Road23.WebAPI.Controllers;
using Road23.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Road23.WebApi.Tests.ControllerTests
{
	public class CategoryControllerTests
	{
		[Fact]
		public void GetAllCategories_ReturnsAllCategories()
		{
			// Arrange
			var mockCategoryRepository = MockRepositoryWrapper.GetCategoryMock();
			var candleRepository = MockRepositoryWrapper.GetCandleMock();

			// // creating controller
			// // Object = gives the instance of Mock, otherwise it is not working
			var categoryController = new CandleCategoryController(mockCategoryRepository.Object, candleRepository.Object);

			// Act
			var result = categoryController.GetCategories() as ObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.NotEmpty(result.Value as IEnumerable<CandleCategoryFullVM>);
		}
	}
}
