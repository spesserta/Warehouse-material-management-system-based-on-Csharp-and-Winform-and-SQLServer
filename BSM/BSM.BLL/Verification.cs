using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSM.BLL
{
    public class Verification
    {
        // 验证 4 位数字编码（可用于：分类编码、简单编码）
        public bool IsCode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.
                IsMatch(str_postalcode, @"^\d{4}$");
        }

        // 验证密码（修复原正则BUG → 支持字母+数字组合）
        public bool IsPwdCode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.
                IsMatch(str_postalcode, @"^[A-Za-z0-9]+$");
        }

        // 验证手机号
        public bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.
                IsMatch(str_handset, @"^[1]+[3,4,5,6,7,8,9]+\d{9}$");
        }

        // 验证是否为中文
        public bool IsChinese(string str_chinese)
        {
            return System.Text.RegularExpressions.Regex.
                IsMatch(str_chinese, @"^[\u4e00-\u9fa5]{0,}$");
        }

        // 验证身份证（你仓库系统基本用不到，保留不影响）
        public bool IsIDcard(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.
                IsMatch(str_postalcode, @"^[1,2,3,4,5,6,7,8,9]+\d{17}$");
        }

        // 验证 4 位数字账号
        public bool IsAccountCode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.
                IsMatch(str_postalcode, @"^\d{4}$");
        }

        // 验证纯数字（你的物料库存、数量、编号必须用这个！）
        public bool IsNumber(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.
                IsMatch(str_postalcode, @"^[0-9]*$");
        }

        // 我给你额外加一个上位机必备：验证物料编号（字母+数字组合，如 M001）
        public bool IsMaterialCode(string code)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(code, @"^[A-Za-z0-9]+$");
        }
    }
}