using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Person.API.Models.Queries {
	public record SearchPerson {
		public SearchMethod Method { get; set; }

		public string Value { get; set; }

		public enum SearchMethod {
			Email,
			CPF,
			UUID
		}

		public static Expression<Func<Entities.Person, bool>> SearchCondition(SearchMethod method, string value) {
			return method switch {
				SearchMethod.Email => x => x.Email == value,
				SearchMethod.CPF => x => x.CPF == value,
				SearchMethod.UUID => x => x.UUID == Guid.Parse(value),
				_ => throw new ArgumentOutOfRangeException(nameof(method)),
			};
		}
	}
}
