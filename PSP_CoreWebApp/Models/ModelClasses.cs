using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PSP_CoreWebApp.Models
{

	public class Category
	{
		[Key]
		public int CategoryRowId { get; set; }
		[Required(ErrorMessage ="Category Id must")]

		public string CategoryId { get; set; }
		[Required(ErrorMessage = "Category Id must")]

		public string CategoryName { get; set; }
		[Required(ErrorMessage = "Category Name must")]

		public int BasePrice { get; set; }

		public ICollection<Product> Products { get; set; }
	}

	public class Product
	{
		[Key]
		public int ProductRowId { get; set; }
		[Required (ErrorMessage="Product Id must")]
		public string ProductId { get; set; }
		[Required(ErrorMessage = "Product Id must")]
		public string ProductName { get; set; }
		[Required(ErrorMessage = "Product Name must")]
		public string Manufacturer { get; set; }
		[Required(ErrorMessage = "Product Manufacturer must")]
		public string Description { get; set; }
		[Required(ErrorMessage = "Product Description must")]
		public int Price { get; set; }
		[ForeignKey ("CategoryRowId")]
		public int CategoryRowId { get; set; }
		public Category Category { get; set; }
	}

}
