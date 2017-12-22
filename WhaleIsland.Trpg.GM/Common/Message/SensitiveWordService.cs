using System;
using System.Collections;
using System.Collections.Generic;
using WhaleIsland.Trpg.GM.Common.Cache.Generic;

namespace WhaleIsland.Trpg.GM.Common.Message
{
    /// <summary>
    /// 敏感词组件
    /// </summary>
    public class SensitiveWordService
    {
        private class BadWordsFilter
        {
            private HashSet<string> hash = new HashSet<string>();
            private byte[] fastCheck = new byte[char.MaxValue];
            private byte[] fastLength = new byte[char.MaxValue];
            private BitArray charCheck = new BitArray(char.MaxValue);
            private BitArray endCheck = new BitArray(char.MaxValue);
            private int maxWordLength = 0;
            private int minWordLength = int.MaxValue;

            public void AddKey(string word)
            {
                maxWordLength = Math.Max(maxWordLength, word.Length);
                minWordLength = Math.Min(minWordLength, word.Length);

                for (int i = 0; i < 7 && i < word.Length; i++)
                {
                    fastCheck[word[i]] |= (byte)(1 << i);
                }

                for (int i = 7; i < word.Length; i++)
                {
                    fastCheck[word[i]] |= 0x80;
                }

                if (word.Length == 1)
                {
                    charCheck[word[0]] = true;
                }
                else
                {
                    fastLength[word[0]] |= (byte)(1 << (Math.Min(7, word.Length - 2)));
                    endCheck[word[word.Length - 1]] = true;
                    hash.Add(word);
                }
            }

            public string Filter(string text, char mask)
            {
                char[] chars = text.ToCharArray();
                int index = 0;

                while (index < text.Length)
                {
                    int count = 1;

                    if (index > 0 || (fastCheck[text[index]] & 1) == 0)
                    {
                        while (index < text.Length - 1 && (fastCheck[text[++index]] & 1) == 0) ;
                    }

                    char begin = text[index];

                    if (minWordLength == 1 && charCheck[begin])
                    {
                        chars[index] = mask;
                    }

                    for (int j = 1; j <= Math.Min(maxWordLength, text.Length - index - 1); j++)
                    {
                        char current = text[index + j];
                        if ((fastCheck[current] & 1) == 0)
                        {
                            ++count;
                        }

                        if ((fastCheck[current] & (1 << Math.Min(j, 7))) == 0)
                        {
                            break;
                        }

                        if (j + 1 >= minWordLength)
                        {
                            if ((fastLength[begin] & (1 << Math.Min(j - 1, 7))) > 0 && endCheck[current])
                            {
                                string sub = text.Substring(index, j + 1);

                                if (hash.Contains(sub))
                                {
                                    int subCount = index + j + 1;
                                    for (int r = index; r < subCount; r++)
                                    {
                                        chars[r] = mask;
                                    }
                                }
                            }
                        }
                    }

                    index += count;
                }
                return new string(chars);
            }

            public bool HasBadWord(string text)
            {
                int index = 0;

                while (index < text.Length)
                {
                    int count = 1;

                    if (index > 0 || (fastCheck[text[index]] & 1) == 0)
                    {
                        while (index < text.Length - 1 && (fastCheck[text[++index]] & 1) == 0) ;
                    }

                    char begin = text[index];

                    if (minWordLength == 1 && charCheck[begin])
                    {
                        return true;
                    }

                    for (int j = 1; j <= Math.Min(maxWordLength, text.Length - index - 1); j++)
                    {
                        char current = text[index + j];

                        if ((fastCheck[current] & 1) == 0)
                        {
                            ++count;
                        }

                        if ((fastCheck[current] & (1 << Math.Min(j, 7))) == 0)
                        {
                            break;
                        }

                        if (j + 1 >= minWordLength)
                        {
                            if ((fastLength[begin] & (1 << Math.Min(j - 1, 7))) > 0 && endCheck[current])
                            {
                                string sub = text.Substring(index, j + 1);

                                if (hash.Contains(sub))
                                {
                                    return true;
                                }
                            }
                        }
                    }

                    index += count;
                }

                return false;
            }
        }

        private static BadWordsFilter _filter;

        static SensitiveWordService()
        {
            Init();
        }

        /// <summary>
        /// Init word
        /// </summary>
        public static void Init()
        {
            _filter = new BadWordsFilter();
            var cacheSet = new ShareCacheStruct<SensitiveWord>();
            cacheSet.Foreach((k, v) =>
            {
                _filter.AddKey(v.Word);
                return true;
            });
        }

        /// <summary>
        /// 检查是否包含敏感词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsVerified(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return _filter.HasBadWord(str);
        }

        /// <summary>
        /// 过滤敏感词
        /// </summary>
        /// <param name="str"></param>
        /// <param name="replaceChar">替换的字符</param>
        /// <returns></returns>
        public string Filter(string str, char replaceChar = '*')
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return _filter.Filter(str, replaceChar);
        }

    }
}
