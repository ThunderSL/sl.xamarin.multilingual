﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Multilingual.Example.Extensions
{
	[ContentProperty("CurrentCulture")]
	public class TranslateExtension : IMarkupExtension
	{
        //Path of your default ressource file
		private const string ResourceId = "Multilingual.Example.Resources.Languages.AppResources";
		private static readonly Lazy<ResourceManager> ResManager = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

		public string CurrentCulture { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (CurrentCulture == null)
				return "";

		    var info = MultilingualCore.Current.CurrentCultureInfo;
			var translation = ResManager.Value.GetString(CurrentCulture, info);

			if (translation == null)
			{
				#if DEBUG
				throw new ArgumentException(
					String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", CurrentCulture, ResourceId, info.Name),
					"Text");
				#else
				translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
				#endif
			}
			return translation;
		}
	}
}