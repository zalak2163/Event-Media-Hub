using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// Controller for managing category-related actions.
    /// Provides operations for creating, updating, deleting, and viewing categories.
    /// </summary>
    public class CategoryPageController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryPageController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Displays the index page for categories.
        /// </summary>
        /// <returns>The Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieves and displays a list of all categories.
        /// </summary>
        /// <returns>A view with a list of categories.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View(await _categoryService.ListCategories());
        }

        /// <summary>
        /// Retrieves and displays the details of a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A view with details of the specified category.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _categoryService.GetCategory(id));
        }

        /// <summary>
        /// Displays the page for creating a new category.
        /// </summary>
        /// <returns>A view for creating a new category.</returns>
        [Authorize]
        public IActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Creates a new category based on the provided details.
        /// </summary>
        /// <param name="categoryDto">The details of the category to create.</param>
        /// <returns>A redirection to the Details page of the newly created category or an error view.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var response = await _categoryService.CreateCategory(categoryDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "CategoryPage", new { id = response.CreatedId });
            }

            return View("Error", new ErrorViewModel { Errors = response.Messages });
        }

        /// <summary>
        /// Retrieves and displays the page for editing a category.
        /// </summary>
        /// <param name="id">The ID of the category to edit.</param>
        /// <returns>A view for editing the specified category.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDto = await _categoryService.GetCategory(id);

            if (categoryDto == null)
            {
                return View("Error");
            }

            return View(categoryDto);
        }

        /// <summary>
        /// Updates the details of an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The updated details of the category.</param>
        /// <returns>A redirection to the Details page of the updated category or an error view.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(int id, CategoryDto categoryDto)
        {
            var response = await _categoryService.UpdateCategory(id, categoryDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "CategoryPage", new { id = id });
            }

            return View("Error", new ErrorViewModel { Errors = response.Messages });
        }

        /// <summary>
        /// Displays a confirmation page for deleting a category.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A confirmation view for deleting the specified category.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var categoryDto = await _categoryService.GetCategory(id);

            if (categoryDto == null)
            {
                return View("Error");
            }

            return View(categoryDto);
        }

        /// <summary>
        /// Deletes a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A redirection to the List page or an error view.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.DeleteCategory(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "CategoryPage");
            }

            return View("Error");
        }
    }
}

