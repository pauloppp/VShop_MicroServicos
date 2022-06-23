using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace VShop_MicroServico.ProdutoWEB.PontoFlutuante
{
	public class CustomFloatingPointModelBinderProvider : IModelBinderProvider
	{
		/// <summary>
		/// Gets or sets the supported <see cref="NumberStyles"/> globally.
		/// Default: <see cref="NumberStyles.Float"/> and <see cref="NumberStyles.AllowThousands"/>.
		/// </summary>
		/// <remarks>
		/// <see cref="SimpleTypeModelBinder"/> uses <see cref="DecimalConverter"/> and similar. Those <see cref="TypeConverter"/>s default to <see cref="NumberStyles.Float"/>.
		/// </remarks>
		public static System.Globalization.NumberStyles SupportedNumberStyles { get; set; } = NumberStyles.Float | NumberStyles.AllowThousands;

		/// <summary>
		/// Gets or sets the <see cref="System.Globalization.CultureInfo"/> to use while parsing globally.
		/// Default: <see cref="CultureInfo.InvariantCulture"/>.
		/// </summary>
		public static CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;

		/// <inheritdoc />
		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			var modelType = context.Metadata.UnderlyingOrModelType;
			var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
			if (modelType == typeof(decimal) ||
				modelType == typeof(double) ||
				modelType == typeof(float))
			{
				return new CustomFloatingPointModelBinder(SupportedNumberStyles, CultureInfo, loggerFactory);
			}
			return null;
		}
	}
}
