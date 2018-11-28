using Newtonsoft.Json;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace MecabWordBreaker.Tests
{
    public class TestWordBreaker
    {
        private MecabWB Wrapper { get; }
        private ITestOutputHelper Output { get; }
        public TestWordBreaker(ITestOutputHelper output)
        {
            this.Output = output;

            var mecabDir = Environment.GetEnvironmentVariable("MECAB_DICT_DIR");
            Assert.True(mecabDir!=null, "MECAB_DICT_DIR needs to be set to a folder which contains mecab dictionary files (char.bin, matrix.bin, etc).");
            output.WriteLine(mecabDir);
            Wrapper = new MecabWB(mecabDir);
        }

        [Theory]
        [InlineData("ありがとうございました", "ありがとう ござい まし た")]
        [InlineData("タレントのヒロミ（53）がフジテレビ系日曜午後9時のレギュラーに、15年ぶりに復活することが25日、同局から発表された。", "タレント の ヒロミ （ 53 ） が フジテレビ 系 日曜 午後 9 時 の レギュラー に 、 15 年 ぶり に 復活 する こと が 25 日 、 同局 から 発表 さ れ た 。")]
        public void WordBreak(string text, string wbText)
        {
            var wb = Wrapper.Break(text);
            Output.WriteLine(wb);
            Assert.Equal(wbText, wb);
        }

        [Theory]
        [InlineData("ありがとうございました", "感動詞 助動詞 助動詞 助動詞")]
        public void Feature(string text, string posTags)
        {
            var temp = Wrapper.BreakAsPOSs(text);
            Output.WriteLine(JsonConvert.SerializeObject(temp));
            Assert.Equal(posTags, string.Join(" ", temp.Select(x => x.PosTag)));
        }
    }
}
