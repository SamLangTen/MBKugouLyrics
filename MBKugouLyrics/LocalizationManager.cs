using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicBeePlugin
{
    class LocalizationManager
    {
        public static string Language { get; set; }
        public static string ServerErrorRecaptcha
        {
            get
            {
                switch (Language)
                {
                    case "zh-Hans":
                        return "酷狗服务器返回错误信息，可能是KgMid触发防机器人验证，请到设置面板设置KgMid";
                    case "en":
                        return "Kugou server returns error message. It might be reCaptcha validation. Please set your own kgMid in configuration panel";
                    default:
                        return "Kugou server returns error message. It might be reCaptcha validation. Please set your own kgMid in configuration panel";
                }

            }

        }

        public static string ConfigPanelKgMidDescription
        {
            get
            {
                switch (Language)
                {
                    case "zh-Hans":
                        return "如果酷狗服务器返回错误信息，可能是自动生成的KgMig触发了防机器人验证，\n请输入自己的kgMid值。\n留空自动生成。";
                    case "en":
                        return "If Kugou server returns error, input another KgMid value to pass.\nKeep null to generate automatically";
                    default:
                        return "If Kugou server returns error, input another KgMid value to pass.\nKeep null to generate automatically";
                }

            }

        }
    }
}
