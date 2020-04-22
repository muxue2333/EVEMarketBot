﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nekonya
{
    public static class EVEUtil
    {
        /// <summary>
        /// 解析.jita 消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool ParseJitaMsg(string msg, out string sourceStr, out List<string> queryStr)
        {
            sourceStr = default;
            queryStr = new List<string>();
            if (string.IsNullOrEmpty(msg)) return false;
            if (!msg.ToLower().StartsWith(".jita ")) return false;
            sourceStr = msg.Substring(5)?.Trim();
            if (string.IsNullOrEmpty(sourceStr)) return false;

            queryStr.Add(sourceStr);
            GetQueryStr(ref sourceStr, ref queryStr);

            return true;
        }

        public static void GetQueryStr(ref string scoreStr, ref List<string> queryStr)
        {
            if (scoreStr.StartsWith("海") && !scoreStr.StartsWith("海军型"))
            {
                if (scoreStr.EndsWith("级"))
                    queryStr.Add(scoreStr.Substring(1,scoreStr.Length -1) + "海军型");
                else
                    queryStr.Add(scoreStr.Substring(1, scoreStr.Length - 1) + "级海军型");
            }

            if (!scoreStr.EndsWith("级") && !scoreStr.EndsWith("海军型"))
            {
                queryStr.Add(scoreStr + "级");
            }
        }

        public static bool ParseBindMsg(string msg, out string key,out string value)
        {
            key = default;
            value = default;
            if (string.IsNullOrEmpty(msg)) return false;
            if (!msg.ToLower().StartsWith(".bind ")) return false;
            var _str = msg.Substring(5)?.Trim();
            if (string.IsNullOrEmpty(_str)) return false;
            var str_arr = _str.Split(',');
            if (str_arr.Length != 2) return false;
            key = str_arr[0].Trim();
            value = str_arr[1].Trim();
            return true;
        }

        public static bool ParseUnBindMsg(string msg, out string key)
        {
            key = default;
            if (string.IsNullOrEmpty(msg)) return false;
            if (!msg.ToLower().StartsWith(".unbind ")) return false;
            var _str = msg.Substring(7)?.Trim();
            if (string.IsNullOrEmpty(_str)) return false;
            key = _str;
            return true;
        }

        public static bool ParseAddAdmin(string msg, out long addQQ)
        {
            addQQ = -1;
            if (string.IsNullOrEmpty(msg)) return false;
            if (!msg.ToLower().StartsWith(".addadmin ")) return false;
            var _str = msg.Substring(9)?.Trim();
            if (string.IsNullOrEmpty(_str)) return false;
            return long.TryParse(_str, out addQQ);
        }

        /// <summary>
        /// 是否含有不安全的SQL字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 是否含有中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IncludeChinese(string str)
        {
            bool flag = false;
            foreach (var a in str)
            {
                if (a >= 0x4e00 && a <= 0x9fbb)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
    }
}
