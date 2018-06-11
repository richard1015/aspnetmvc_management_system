using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HTNResp.BLL
{
    public class Program
    {

        public string testContent(string input)
        {

            HTNResp.BLL.ProjectDictionary bllProject = new BLL.ProjectDictionary();
            List<Model.ProjectDictionary> projectList = bllProject.DataTableToList(bllProject.GetList("").Tables[0]);
            string msg = "";
            if (String.IsNullOrEmpty(input))
                return msg = "字符串为空;";
            input = input.ToLower().Replace(" ", "").Trim();
            string withoutBrace = splitBrace(input);
            if (!checkEmpty(input))
            {
                msg += "括号为空附近错误;";
                return msg;
            }
            if (!checkPair(input))
            {
                msg += "括号未匹配;";
                return msg;
            }
            if (withoutBrace == null)
                msg += "大括号附近错误;";
            else
            {
                string[] listWithoutOr = splitOr(withoutBrace);
                List<string> listWithoutCurve = new List<string>();
                if (listWithoutOr != null)
                {
                    //if it contains 'or' then remove curve
                    listWithoutCurve = getOutCurve(listWithoutOr);
                    if (listWithoutCurve.Count != listWithoutOr.Length)
                    {
                        msg += "小括号附近错误;";
                        return msg;
                    }
                }
                else
                {
                    //else add no 'or' no curve string to list
                    listWithoutCurve.Add(withoutBrace);
                }

                List<string> listWithoutAnd = splitAnd(listWithoutCurve);
                if (listWithoutAnd == null)
                {
                    msg += "AND附近有错误;";
                    return msg;
                }
                List<string> listWithoutAt = splitAt(listWithoutAnd);
                if (listWithoutAt == null)
                {
                    msg += "@或!附近有错误;";
                    return msg;
                }
                List<string> listWithoutOperator = splitOperator(listWithoutAt);
                if (listWithoutOperator == null)
                {
                    msg += "比较运算符附近有错误;";
                    return msg;
                }

                //match database
                List<string> listContentInBracket = getContentInBracket(listWithoutOperator);
                if (listContentInBracket.Count != listWithoutOperator.Count)
                {
                    msg += "[]附近有错误;";
                    return msg;
                }

                for (int i = 0; i < listContentInBracket.Count; i++)
                {
                    string content;
                    string value;
                    if (listContentInBracket.Count != 1)
                    {
                        content = listContentInBracket[i];
                        value = listContentInBracket[i + 1];
                    }
                    else
                    {
                        content = listContentInBracket[i];
                        value = " ";
                    }
                    bool flag = false;
                    foreach (Model.ProjectDictionary m in projectList)
                    {
                        if (m.ProjectName == content)
                        {
                            flag = true;
                            string type = "";
                            if (Regex.IsMatch(value, "^\".+\"$"))
                            {
                                type = "string";
                                i++;
                            }
                            else if (Regex.IsMatch(value, "^\\d+(\\.\\d*)?$"))
                            {
                                type = "int";
                                i++;
                            }
                            else
                            {
                                type = "bool";
                            }
                            if (type != m.ProjectType)
                            {
                                msg += "[" + content + "]后的值类型不匹配;";
                                return msg;
                            }
                            else break;

                        }
                    }
                    if (!flag)
                    {
                        msg += "[]中的值 ( " + content + " ) 不在数据库中;";
                        return msg;
                    }
                }

                bool listQuotation = checkQuotation(listWithoutOperator);
                if (!listQuotation)
                {
                    msg += "引号附近有错误;";
                    return msg;
                }
            }
            return msg;
        }

        //取大括号中的内容
        public string splitBrace(string input)
        {
            string process = null;
            MatchCollection collection = Regex.Matches(input, "^\\{([^\\{\\}]*)\\}$");
            if (collection.Count != 0)
            {
                string inBrace = collection[0].Groups[1].Value;
                return inBrace;
            }
            return process;
        }

        //取OR两边的内容
        public string[] splitOr(string input)
        {
            input = input.ToLower();
            string[] processList = null;
            if (input.Contains("or"))
            {
                processList = Regex.Split(input, "or");
                return processList;
            }
            else return null;

        }

        //取小括号中的内容
        public List<string> getOutCurve(string[] inputList)
        {
            List<string> processList = new List<string>();
            foreach (string s in inputList)
            {
                MatchCollection tempCollection = Regex.Matches(s, "^\\(([^\\(\\)]*)\\)$");
                if (tempCollection.Count == 0)
                    continue;
                else
                {
                    string inCurve = tempCollection[0].Groups[1].Value;
                    processList.Add(inCurve);
                }
            }
            return processList;
        }

        //取AND边的内容
        public List<string> splitAnd(List<string> inputList)
        {
            List<string> processList = new List<string>();
            foreach (string s in inputList)
            {
                if (s.Contains("and"))
                {
                    string[] tempList = Regex.Split(s, "and");
                    if (!Regex.IsMatch(tempList[0], ".*[\"\\]\\d]$") || !tempList[1].StartsWith("["))
                        return null;
                    processList.AddRange(tempList);
                }
                else
                {
                    processList.Add(s);
                }
            }
            return processList;
        }

        //取@、!两边的内容
        public List<string> splitAt(List<string> inputList)
        {
            List<string> processList = new List<string>();
            foreach (string s in inputList)
            {
                if (Regex.IsMatch(s, "\\w*(@|!)\\w*"))
                {
                    string[] tempList = Regex.Split(s, "@|!");
                    if (!tempList[0].EndsWith("]") || !tempList[1].StartsWith("\""))
                        return null;
                    processList.AddRange(tempList);
                }
                else
                {
                    processList.Add(s);
                }
            }
            return processList;
        }

        //取运算符两边的内容
        public List<string> splitOperator(List<string> inputList)
        {
            List<string> processList = new List<string>();
            foreach (string s in inputList)
            {
                if (Regex.IsMatch(s, "\\w*(<|=|>)\\w*"))
                {
                    //NOT check << >> >= <= and the legislation of the operand of the operator
                    if (s.Contains("<<") || s.Contains(">>") || s.Contains("><") || s.Contains("==") || s.Contains("=>") || s.Contains("=<"))
                        return null;
                    string tempString = s.Replace("=", "-").Replace("<", "-").Replace(">", "-").Replace("--", "-");
                    string[] tempList = Regex.Split(tempString, "\\-");
                    if (!tempList[0].EndsWith("]") || !Regex.IsMatch(tempList[1], "^\\d+(\\.\\d*)?$"))
                        return null;
                    processList.AddRange(tempList);
                }
                else
                {
                    processList.Add(s);
                }
            }
            return processList;
        }

        //获取方括号中内容
        public List<string> getContentInBracket(List<string> inputList)
        {
            List<string> processList = new List<string>();
            foreach (string s in inputList)
            {
                MatchCollection tempColletion = Regex.Matches(s, "^\\[([^\\[\\]]*)\\]$");
                if (tempColletion.Count != 0)
                {
                    string tempContent = tempColletion[0].Groups[1].Value;
                    processList.Add(tempContent);
                }
                else
                {
                    processList.Add(s);
                }
            }
            return processList;
        }

        //检查引用
        public bool checkQuotation(List<string> inputList)
        {
            foreach (string s in inputList)
            {
                if (s.Contains("\""))
                {
                    if (!Regex.IsMatch(s, "^\"([^\"])*\"$")) return false;
                }
                else continue;
            }
            return true;
        }

        //检查括号内容为空
        public bool checkEmpty(string input)
        {
            if (Regex.IsMatch(input, ".*\\{\\}.*") || Regex.IsMatch(input, ".*\\[\\].*") || Regex.IsMatch(input, ".*\\(\\).*"))
                return false;
            return true;
        }

        //检查括号数目
        public bool checkPair(string input)
        {
            string bracesPattern = @"^\{(.*)\}$";
            MatchCollection collection = Regex.Matches(input, bracesPattern);
            GroupCollection group;
            if (collection.Count == 0)
            {
                return false;
            }
            else
            {
                group = collection[0].Groups;
                string inBrace = group[1].Value;
                int squareBracketCountL = Regex.Matches(input, @"\[").Count;
                int squareBracketCountR = Regex.Matches(input, @"\]").Count;
                int parentheseCountL = Regex.Matches(input, @"\(").Count;
                int parentheseCountR = Regex.Matches(input, @"\)").Count;
                if (squareBracketCountL != squareBracketCountR || parentheseCountL != parentheseCountR)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
