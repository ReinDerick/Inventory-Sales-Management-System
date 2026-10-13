using InventoryManagement.Data;
using InventoryManagement.Models.DTO;
using InventoryManagement.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Inventory_SalesController : ControllerBase
    {

        private readonly InventoryManagementDbContext dbcontext;

        public Inventory_SalesController(InventoryManagementDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet("ViewProducts")]
        public IActionResult GetAllProduct()
        {
            var getAllProduct = dbcontext.productManagement.OrderByDescending(p => p.ProductCode).ToList();
            {
                try
                {
                    if (getAllProduct == null || !getAllProduct.Any())
                    {
                        return NotFound(new { message = $"No products found." });
                    }

                    var productList = getAllProduct.Select(p => new ProductManagementDto
                    {
                        ProductCode = p.ProductCode,
                        ProductName = p.ProductName,
                        ProductPrice = p.ProductPrice,
                        ProductQuantity = p.ProductQuantity,
                        IsActive = p.IsActive,
                        ProductCategory = p.ProductCategory
                    }).ToList();
                    return Ok(productList);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Internal server error: {ex.Message}" });
                }
            }
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductManagementDto productManagementDto)
        {
            try
            {
                var product = dbcontext.productManagement.FirstOrDefault(p => p.ProductCode == productManagementDto.ProductCode || p.ProductName == productManagementDto.ProductName);
                if (product != null)
                {
                    return BadRequest(new { message = $"Product with the same ID already exists." });
                }

                var productInventory = new ProductManagement
                {
                    ProductId = Guid.NewGuid(),
                    ProductCode = productManagementDto.ProductCode,
                    ProductName = productManagementDto.ProductName,
                    ProductPrice = productManagementDto.ProductPrice,
                    ProductQuantity = productManagementDto.ProductQuantity,
                    IsActive = productManagementDto.IsActive,
                    ProductCategory = productManagementDto.ProductCategory
                };

                dbcontext.productManagement.Add(productInventory);
                dbcontext.SaveChanges();

                return Ok(new { message = "Product Added Successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("SearchProducts/{ProductName}")]
        public IActionResult SearchProducts([FromRoute] string ProductName)
        {
            var getProduct = dbcontext.productManagement.FirstOrDefault(p => p.ProductName == ProductName);
            {
                try
                {
                    if (getProduct == null)
                    {
                        return NotFound(new { message = $"No products found." });
                    }

                    var getProductList = new ProductManagementDto
                    {
                        ProductCode = getProduct.ProductCode,
                        ProductName = getProduct.ProductName,
                        ProductPrice = getProduct.ProductPrice,
                        ProductQuantity = getProduct.ProductQuantity,
                        IsActive = getProduct.IsActive,
                        ProductCategory = getProduct.ProductCategory
                    };

                    return Ok(getProductList);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Internal server error: {ex.Message}" });
                }
            }
        }

        [HttpPut("UpdateProduct/{ProductCode}")]

        public IActionResult UpdateProduct([FromRoute] string ProductCode, [FromBody] ProductManagementDto productManagementDto)
        {
            try
            {
                var product = dbcontext.productManagement.FirstOrDefault(p => p.ProductCode == ProductCode);

                if (product == null)
                {
                    return NotFound(new { message = $"Product not found." });
                }
                product.ProductName = productManagementDto.ProductName;
                product.ProductPrice = productManagementDto.ProductPrice;
                product.ProductQuantity = productManagementDto.ProductQuantity;
                product.IsActive = productManagementDto.IsActive;
                product.ProductCategory = productManagementDto.ProductCategory;

                dbcontext.SaveChanges();

                return Ok(new { message = "Product Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("DeleteProduct/{ProductCode}")]

        public IActionResult DeleteProduct([FromRoute] string ProductCode)
        {
            try
            {
                var product = dbcontext.productManagement.FirstOrDefault(p => p.ProductCode == ProductCode);
                if (product == null)
                {
                    return NotFound(new { message = $"Product not found." });
                }

                product.IsActive = false; // Mark the product as inactive instead of deleting it

                dbcontext.SaveChanges();

                return Ok(new { message = "Product Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("ViewProductsByCategory/{ProductCategory}")]

        public IActionResult GetProductsByCategory([FromRoute] string ProductCategory)
        {
            var getAllProduct = dbcontext.productManagement.Where(p => p.ProductCategory == ProductCategory).OrderByDescending(p => p.ProductCode).ToList();
            {
                try
                {
                    if (getAllProduct == null || !getAllProduct.Any())
                    {

                        return NotFound(new { message = $"No products found." });

                    }

                    var productList = getAllProduct.Select(p => new ProductManagementDto
                    {
                        ProductCode = p.ProductCode,
                        ProductName = p.ProductName,
                        ProductPrice = p.ProductPrice,
                        ProductQuantity = p.ProductQuantity,
                        IsActive = p.IsActive,
                        ProductCategory = p.ProductCategory

                    }).ToList();

                    return Ok(productList);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Internal server error: {ex.Message}" });
                }
            }
        }

        [HttpGet("ViewActiveProducts/{ProductCategory}")]

        public IActionResult GetActiveProduct([FromRoute] string ProductCategory)
        {
            var getAllProduct = dbcontext.productManagement.Where(p => p.IsActive == true && p.ProductCategory == ProductCategory).OrderByDescending(p => p.ProductCode).ToList();
            {
                try
                {
                    if (getAllProduct == null || !getAllProduct.Any())
                    {

                        return NotFound(new { message = $"No products found." });

                    }

                    var productList = getAllProduct.Select(p => new ProductManagementDto
                    {
                        ProductCode = p.ProductCode,
                        ProductName = p.ProductName,
                        ProductPrice = p.ProductPrice,
                        ProductQuantity = p.ProductQuantity,
                        IsActive = p.IsActive,
                        ProductCategory = p.ProductCategory

                    }).ToList();

                    return Ok(productList);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Internal server error: {ex.Message}" });
                }
            }
        }
    }
}
