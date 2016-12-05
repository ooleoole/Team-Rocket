﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Domain.Entities;
using Domain.Services;


namespace FootballManager.App.Converters
{
    public class PlayerIdToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PlayerService playerService = new PlayerService();
            if (value == null)
            {
                return null;
            }
            else
            {
                var player = playerService.FindById((Guid)value);
                return player?.Name;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
