﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Helpers
{
    public static class RandomHelper
    {
        #region 中文 - 常用姓氏_ChineseFirstName

        /// <summary>
        /// 中文 - 常用姓氏
        /// </summary>
        private static readonly string _ChineseFirstName = "赵钱孙李周吴郑王冯陈楮卫蒋沈韩杨朱秦尤许何吕施张孔曹严华金魏陶姜戚谢邹喻柏水窦章云苏潘葛奚范彭郎鲁韦昌马苗凤花方俞任袁柳酆鲍史唐费廉岑薛雷贺倪汤滕殷罗毕郝邬安常乐于时傅皮卞齐康伍余元卜顾孟平黄和穆萧尹姚邵湛汪祁毛禹狄米贝明臧计伏成戴谈宋茅庞熊纪舒屈项祝董梁杜阮蓝闽席季麻强贾路娄危江童颜郭梅盛林刁锺徐丘骆高夏蔡田樊胡凌霍虞万支柯昝管卢莫经房裘缪干解应宗丁宣贲邓郁单杭洪包诸左石崔吉钮龚程嵇邢滑裴陆荣翁荀羊於惠甄麹家封芮羿储靳汲邴糜松井段富巫乌焦巴弓牧隗山谷车侯宓蓬全郗班仰秋仲伊宫宁仇栾暴甘斜厉戎祖武符刘景詹束龙叶幸司韶郜黎蓟薄印宿白怀蒲邰从鄂索咸籍赖卓蔺屠蒙池乔阴郁胥能苍双闻莘党翟谭贡劳逄姬申扶堵冉宰郦雍郤璩桑桂濮牛寿通边扈燕冀郏浦尚农温别庄晏柴瞿阎充慕连茹习宦艾鱼容向古易慎戈廖庾终暨居衡步都耿满弘匡国文寇广禄阙东欧殳沃利蔚越夔隆师巩厍聂晁勾敖融冷訾辛阚那简饶空曾毋沙乜养鞠须丰巢关蒯相查后荆红游竺权逑盖益桓公万俟司马上官欧阳夏侯诸葛闻人东方赫连皇甫尉迟公羊澹台公冶宗政濮阳淳于单于太叔申屠公孙仲孙轩辕令狐锺离宇文长孙慕容鲜于闾丘司徒司空丌官司寇仉督子车颛孙端木巫马公西漆雕乐正壤驷公良拓拔夹谷宰父谷梁晋楚阎法汝鄢涂钦段干百里东郭南门呼延归海羊舌微生岳帅缑亢况后有琴梁丘左丘东门西门商牟佘佴伯赏南宫墨哈谯笪年爱阳佟";

        //1：李	2：王	3：张	4：刘	5：陈
        //6：杨	7：赵	8：黄	9：周	10：吴
        //11：徐	12：孙	13：胡	14：朱	15：高
        //16：林	17：何	18：郭	19：马	20：罗
        //21：梁	22：宋	23：郑	24：谢	25：韩
        //26：唐	27：冯	28：于	29：董	30：萧
        //31：程	32：曹	33：袁	34：邓	35：许
        //36：傅	37：沈	38：曾	39：彭	40：吕
        //41：苏	42：卢	43：蒋	44：蔡	45：贾
        //46：丁	47：魏	48：薛	49：叶	50：阎
        //51：余	52：潘	53：杜	54：戴	55：夏
        //56：钟	57：汪	58：田	59：任	60：姜
        //61：范	62：方	63：石	64：姚	65：谭
        //66：廖	67：邹	68：熊	69：金	70：陆
        //71：郝	72：孔	73：白	74：崔	75：康
        //76：毛	77：邱	78：秦	79：江	80：史
        //81：顾	82：侯	83：邵	84：孟	85：龙
        //86：万	87：段	88：漕	89：钱	90：汤
        //91：尹	92：黎	93：易	94：常	95：武
        //96：乔	97：贺	98：赖	99：龚	100：文

        //2016年中国人口最多的前100至200大姓
        //101：庞	102：樊	103：兰	104：殷	105：施
        //106：陶	107：洪	108：翟	109：安	110：颜
        //111：倪	112：严	113：牛	114：温	115：芦
        //116：季	117：俞	118：章	119：鲁	120：葛
        //121：伍	122：韦	123：申	124：尤	125：毕
        //126：聂	127：丛	128：焦	129：向	130：柳
        //131：邢	132：路	133：岳	134：齐	135：沿
        //136：梅	137：莫	138：庄	139：辛	140：管
        //141：祝	142：左	143：涂	144：谷	145：祁
        //146：时	147：舒	148：耿	149：牟	150：卜
        //151：路	152：詹	153：关	154：苗	155：凌
        //156：费	157：纪	158：靳	159：盛	160：童
        //161：欧	162：甄	163：项	164：曲	165：成
        //166：游	167：阳	168：裴	169：席	170：卫
        //171：查	172：屈	173：鲍	174：位	175：覃
        //176：霍	177：翁	178：隋	179：植	180：甘
        //181：景	182：薄	183：单	184：包	185：司
        //186：柏	187：宁	188：柯	189：阮	190：桂
        //191：闵	192：欧阳	193：解	194：强	195：柴
        //196：华	197：车	198：冉	199：房	200：边

        //2016年中国人口最多的前200至300大姓
        //201：辜	202：吉	203：饶	204：刁	205：瞿
        //206：戚	207：丘	208：古	209：米	210：池
        //211：滕	212：晋	213：苑	214：邬	215：臧
        //216：畅	217：宫	218：来	219：嵺	220：苟
        //221：全	222：褚	223：廉	224：简	225：娄
        //226：盖	227：符	228：奚	229：木	230：穆
        //231：党	232：燕	233：郎	234：邸	235：冀
        //236：谈	237：姬	238：屠	239：连	240：郜
        //241：晏	242：栾	243：郁	244：商	245：蒙
        //246：计	247：喻	248：揭	249：窦	250：迟
        //251：宇	252：敖	253：糜	254：鄢	255：冷
        //256：卓	257：花	258：仇	259：艾	260：蓝
        //261：都	262：巩	263：稽	264：井	265：练
        //266：仲	267：乐	268：虞	269：卞	270：封
        //271：竺	272：冼	273：原	274：官	275：衣
        //276：楚	277：佟	278：栗	279：匡	280：宗
        //281：应	282：台	283：巫	284：鞠	285：僧
        //286：桑	287：荆	288：谌	289：银	290：扬
        //291：明	292：沙	293：薄	294：伏	295：岑
        //296：习	297：胥	298：保	299：和	300：蔺
        #endregion

        #region 中文 - 常用名字_ChineseSecondName - 女

        /// <summary>
        /// 中文 - 常用名字 - 女
        /// </summary>
        private static readonly string _ChineseSecondNameFamale = "嘉琼桂娣叶璧璐娅琦晶妍茜秋珊莎锦黛青倩婷姣婉娴瑾颖露瑶怡婵雁蓓纨仪荷丹蓉眉君琴蕊薇菁梦岚苑婕馨瑗琰韵融园艺咏卿聪澜纯毓悦昭冰爽琬茗羽希宁欣飘育滢馥筠柔竹霭凝晓欢霄枫芸菲寒伊亚宜可姬舒影荔枝思丽秀娟英华慧巧美娜静淑惠珠翠雅芝玉萍红娥玲芬芳燕彩春菊勤珍贞莉兰凤洁梅琳素云莲真环雪荣爱妹霞香月莺媛艳瑞凡佳";

        #endregion

        #region 中文 - 常用名字_ChineseSecondName - 男

        /// <summary>
        /// 中文 - 常用名字 - 男
        /// </summary>
        private static readonly string _ChineseSecondMale = "涛昌进林有坚和彪博诚先敬震振壮会群豪心邦承乐绍功松善厚庆磊民友裕河哲江超浩亮政谦亨奇固之轮翰朗伯宏言若鸣朋斌梁栋维启克伦翔旭鹏泽晨辰士以建家致树炎德行时泰盛雄琛钧冠策腾伟刚勇毅俊峰强军平保东文辉力明永健世广志义兴良海山仁波宁贵福生龙元全国胜学祥才发成康星光天达安岩中茂武新利清飞彬富顺信子杰楠榕风航弘";

        #endregion

        #region 中文 - 常用汉字_ChinesePopular

        private static readonly string _ChinesePopular = "一乙乃了力丁刀刁二又人入七十几口干工弓久己土大丈女己巾勺丸也于弋巳兀三下上乞士夕千子寸小山川巳才凡公孔亢勾。中之丹井介今内及太天屯斗斤牛丑支允元勿午友尤尹引文月日牙王云匀牛四丑仁什切升收壬少心手日氏水尤仍双尺仇止才不互分匹化卡卞肥反夫巴幻户方木比毛仆丰火片古切可瓜甘刊五丘加去句叫外巧巨玉甲令加占主巨冬他代只仗另句召尼正田旦奴凸立叮仝伏台奶凹五外央右未永以戊玉瓦由幼仕巧丘仙兄司且史左世出市玄仔冉穴示生申充主仞仟册加去只叫求正甲申石匝甩丙平母弘末包本弗北必丕半布皿目乏禾皮疋矛乎付兄卉戊民冰玄白卯伉光匡共各考交件价企伍伎仰吉圭曲机艮六仲吉州朱兆决匠地旨朵吏列年劣同打汀至臼灯竹老舟伎吊吏圳的宅机老肉虫伊仰伍印因宇安屹有羊而耳衣亦吃夷奸聿丞企休任先全再冲刑向在夙如宅守字存寺式戌收早旭旬曲次此求系肉臣自舌血行圳西休交件企匠吉尖而至色伏后名回好妃帆灰牟百份米伐亥卉冰刑合向旭朴系行汜复克告改攻更杆谷况伽估君吴吸吾圻均坎研完局岐我扣杞江究见角言住占低佃况里冷伶利助努君吝昌壮妓妞局弄廷弟彤志托杖杜呆李江男究良见角具皂里舟佟你体足甸町豆吞玎位佐佘冶吾吟吴吻完尾巫役忘我修言邑酉吟吴研呆角七伸佐作些伽些初吹呈坐孝宋岐希岑床序巡形忍成杏材杉束村杞步汝汐池私秀赤足身车辰系占伺住余助劭劬邵吸坐壮妆局床志汕江灶见即却克早何布伯伴佛兵判别含坊坂吵宏旱每甫牡况免孚孝尾巫希庇形忙杏呆步汛贝儿供侃刻卦固坤姑官冈庚快抗昆果空亟其具券卷奇委季宜居届岢岸杰佳京侄佳来例制到兔两典卷周呢坦奈妮宙定居屉帖底店征忝忠念技投政枝东林汰决玖知的直纠金两乳侏佰侗佻佬具冽卓拈妲妯宕岱岭帖帙底抒林杼沓炉竺长依侑味夜委宜宛岳岸岩往亚武於易昂旺沅沃汪物艾卧佯儿咏抑昀炎杳事享侍使侈然刹刺协卒洽沁取受步垂奇始炊姓妻妾尚屈弦所承昌升昔松欣沙沈社舍炊采长青幸亟徇佳舍儿争其刷券制效卷姐姒姗季炙宗届岫征承昔析枕状八并佩函和命坡坪奉孟帛水府佛彼忽或戽房扮枇扶放昏朋服明杭杯枚板沛沐汾版牧虎门阜杷盲非冠奎皈客故柑柯况看科肝革屋癸砍禹轨页九亭亮柱俊侣冒段劲南姬姜姣宦帝度痔建峙待律怠急招拒拓拙拉昭架柱柳注治炭界皆突纪纣耐肚致计订军酊俐胃百厘咨姝姿柁沱炭妆纣重珏盅眈俄俞勇威娃姻姚姨屋幽彦奕哀哇玟怡押映昱韦油泳沿姚畏烟盈禹约耶衍要页音昱易柚胤易信侠系俗促俏前则奏型姹妊姝姿室宣巷咱哉思性施昨是春星查柴栅柔染泉帅甚相省砂祈秋穿肖重首酉食香侵俟峙旭注沐炷祉贞昌泓侯保便冒勉匍奔品佩杯封哈皇拔抱怕柏柄泌法泡炳玫盆眉红美虹秒表负面风飞眄胃勃厚咸叛孩奂屏枰某河泛赴库恭拱格桂根耕耿股肯贡高个刚哥宫径挂皋径徒倜恬拯指拿料旅晋朕桌桔桃桐洞流洛酒烈特玲珍真矩祝秩租站级纸纳纽者肩芝记讨酌酒针钉只挑借倒值俱倪倘伦兼唐哲娘旃娟娜展峻准凌洲套爹特留俩倜庭恫耻烙料栗株津玳畜砧恩按案鸟洋秧翁纹耘育芽芸蚊袁烟倚原员埃宴峨倚娱容峪晏移益差师席座徐恰息恕肩持拳拾时书曹校朔桑栽殊气洽珊祠神祖秦秤索素纱纾纯虔讫训财起轩芩闪迅倩幸修仓城夏孙宰容射峡厝叟奚畜春乘借准淞宵指拭牲洵洳狩兹珊炸租站宸挈旁晃桓活洪畔亩眠破炮秘粉纺肺肥航般芳芙花配马侯倍俯俸们圃埋娩峰肪涵畔埔害恢恍恒柏派洹玻泌珉祜呗国寇昆康苦袍规贯够勘崞岗梗珙偕假健停侦剪动翎念基坚堂堆婧寄专张得教救朗条梁梯械梨浙浪珠略皎眷窕竟第终累舵苓架诀近钓顶鸟将那庶振挺捉捐甜祭趾囵堆凌崃带帐徕悌画梁梃桶町娄伟偶务唯问婉寅尉帷庸悟悠悦敖晚梧浴眼研移胃苑英迎野鱼欲浣翌圉乾做区卿参售启商唱娶妇宿崇崎崔常强从悄叙旋晨晟族消爽犀祥绅细紫组绍婧羞习邢舷船茄若处术袖设讼责赦雀雪顷彩常孰侦匙圊执将专就峥崧巢庶彩悉施曹浙笙钏阡凰毫培婚婆妇密彬彪患斌曼海浩烽班瓶毕盒符邦胡背胞胖舶范茅苗袍被觅访货返贩闭麦麻邦壶票冕副埠屏涵捕敏皓梅第珩艴苹敢款淦筐给贵辜开凯昆诒询几蛟植堵堤奠岚帧掌掘捷掏掎探接敦景智晶替朝椒棠栈殖淘添淡净焦街诊理荔眨贴屠贷轸迢迪迦量钧钮间集杰劳单婷喋传塘塔暖楠殿渡汤帏幄惟掩椅涯液渊焰为异砚围茵越阮轶雁雅寓云雯媛喻贻婺焱琬琰畲劳博堡报富寒嵋帽幅帮弼复彭偏整理惠扉排斑酣普棉棒棚涵混淼淮淝牌画番发皓脉茗评贺费买贸迫邯闵防阪黄傅傍媒媚黑瓿匮块干感揆手楷港琨莞夸鼓该贾传仅涂塔塘廊谦提敬斟极楠殿渡绢经茎莒获莅庄莉蜀里装解詹鼎贾路迹退铃钜陀电雷靖顿暖桢路嫁农贮贷贴轸迪钠湍琳当略铃鼓励庸园圆奥爱意扬援握榆业杨椰渝渭游炜爷烟兽犹煜碗筠义肄莞莠虞蛾裕诣郁钰雍阿预饮衙莹蓊晕渥琬琰畹筵裔淡催传勤势嗣塞嵩厦新喧楸楚岁测凑煦琴琪琦睡祺稔稠筮粲绣群圣莎裙诩诗试诠详资载送铅阻雌颂驰熙暄琼塞嵩想桢椿岁渚煮琛庄裟输轼幕汇惶挥描换楣枫湖浑渺涣煤琶琥盟睦碑禀聘腑荷莫号蜂补话酩附颁饭晕募焕 廓愧沟管纲诰闺魁构歌恺斡荣嘏通连这甄兢喜团图奖嫡对僚侥嶂崭彰廖熊溜监祯种端箕筝精绿紧绫纶置璋畅榔糍滇尔莱赵铬领瑙奁闻嫣愿温源溢尔瑜瑛瑗玮与苑诱语郢银摇榕荣温荥溶菸菀鞅瑛僖逑速逍肾寿塾尘嫩实像侨岖慈沧溶溪熊狮瑞瑟硕算粹绸综绰绮翠菁菜菖裳认誓诵说诚轻菘造速衔铨限需韶饲饰旗畅荣榕齐熏僦尝墅奖实寨慎准溯搴逢梦仆幕滑瑚珲碧福绵翡腑萍菩蜜裴豪宾辅郝铭阀陌颇饮凤鸣榜槐滏宽广慷惯概瑰葛葵课逵郭稿靠锆俭著价厉剧剑刘妖履帜弹徵德摘敌整暂椿闾乐楼樟滴渐涨浆练缔蒂骀落董葶蝶调谆谈诋诤谅论质驼践轮逮进醇铝阵震霆驾驻稽稻稷节剪几鲁黎侬涤墩幢嶙锻褚亿仪影慰忧乐样欧毅演渔熬熠瑶缘纬腰万莹叶苇谊逸邮阅院鞍颐养欲颍粗缓卫葳骑妩鉴署啸增婵审层厂厨厮庆摧数枢熟热线肠兴萱冲褚谁请贤赏赐趣娴醉锐销爽霄驷确磁穷箭箱竖辍帜漩渐箴节绪翦葸诤谆质醇罚划哗坟墨币庙废慧慕慢暮樊模流浒汉满漫漂玛缓编腹铺葆葡复卖赋辉辈部锋陛盘码篇范麾劈幡慧摩沪漠磐褒弼荭窥糕膏盖钢龟购器垦横橄篙馆坛导惮战撰整历瞳昙暨机橘洁潭灯瑾璋庐积筑蒸诸谛诺练猪赖蹄辑道达都录锦锭陆陶陵霓霖静颊头雕疆腿臻赚骆莅俦橙润澈笃缙萤萤卫谓谒谙谕豫逾游运阴余壅蓉蓊勋儒器学宪熹憧晓桥樵橙橡树潮甑莳璇聪融亲谌谐输遂醒";

        #endregion

        #region 数字
        private static readonly string _Nums = "0123456789";
        #endregion

        #region 英文字母(小写)
        private static readonly string _CharsL = "abcdefghijklmnopqrstuvwxyz";
        #endregion

        #region 英文字母(大写)
        private static readonly string _CharsU = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        #endregion

        /// <summary>
        /// 格式类型
        /// </summary>
        public enum Scheme : byte
        {
            /// <summary>
            /// 纯数字. eg: 123
            /// </summary>
            Num,
            /// <summary>
            /// 纯字符. eg: abcABC
            /// </summary>
            Char,
            /// <summary>
            /// 纯英文字符(小写). eg: abc 
            /// </summary>
            CharL,
            /// <summary>
            /// 纯英文字符(大写). eg: ABC
            /// </summary>
            CharU,
            /// <summary>
            /// 数字 + 字母. eg: 123abcABC
            /// </summary>
            NumChar,
            /// <summary>
            /// 数字 + 字符(小写). eg: eg: 123abc
            /// </summary>
            NumCharL,
            /// <summary>
            /// 数字 + 字符(大写). eg: 123ABC
            /// </summary>
            NumCharU,
            /// <summary>
            /// 中文字符. eg: 也无风雨也无晴
            /// </summary>
            Chinese,
        }

        /// <summary>
        /// 根据格式类型组合字符集
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private static string getSource(Scheme scheme)
        {
            switch (scheme)
            {
                case Scheme.Num:
                    return _Nums;
                case Scheme.Char:
                    return _CharsL + _CharsU;
                case Scheme.CharL:
                    return _CharsL;
                case Scheme.CharU:
                    return _CharsU;
                case Scheme.NumChar:
                    return _Nums + _CharsL + _CharsU;
                case Scheme.NumCharL:
                    return _Nums + _CharsL;
                case Scheme.NumCharU:
                    return _Nums + _CharsU;
                case Scheme.Chinese:
                    return _ChinesePopular;
                default:
                    throw new ArgumentException("RandomHelper : Format is not defined.");
            }
        }

        /// <summary>
        /// 生成指定范围内的一个随机整数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int Gen(int min, int max)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(min, max);
        }

        /// <summary>
        /// 生成定长随机字符串
        /// </summary>
        /// <param name="scheme">格式</param>
        /// <param name="len">结果长度</param>
        /// <returns></returns>
        public static string Gen(Scheme scheme, int len)
        {
            var source = getSource(scheme);
            var rand = new Random(Guid.NewGuid().GetHashCode());

            var sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb.Append(source[rand.Next(0, source.Length)]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成随机长度字符串
        /// </summary>
        /// <param name="minlen">最小长度</param>
        /// <param name="maxlen">最大长度</param>
        /// <returns></returns>
        public static string Gen(Scheme scheme, int minlen, int maxlen)
        {
            return Gen(scheme, Gen(minlen, maxlen));
        }

        /// <summary>
        /// 生成定长随机字符串列表
        /// </summary>
        /// <param name="scheme">生成格式</param>
        /// <param name="len">生成字符串长度</param>
        /// <param name="count">生成个数</param>
        /// <returns></returns>
        public static List<string> Gens(Scheme scheme, int len, int count)
        {
            var result = new List<string>();

            for (int i = 0; i < count; i++)
            {
                result.Add(Gen(scheme, len));
            }

            return result;
        }

        public static List<string> Gens(Scheme scheme, int minlen, int maxlen, int count)
        {
            var result = new List<string>();

            for (int i = 0; i < count; i++)
            {
                result.Add(Gen(scheme, minlen, maxlen));
            }

            return result;
        }

        /// <summary>
        /// 生成颜色对象
        /// </summary>
        /// <param name="alpha">颜色 alpha, 默认(最大） 255. 不透明</param>
        /// <returns></returns>
        public static Color GenColor(int alpha = 255)
        {
            if (alpha > 255 || alpha < 1)
            {
                throw new ArgumentException();
            }

            var rnd = new Random(Guid.NewGuid().GetHashCode());

            var r = rnd.Next(0, 255);
            var g = rnd.Next(0, 255);
            var b = rnd.Next(0, 255);

            return Color.FromArgb(alpha, r, g, b);
        }

        /// <summary>
        /// 生成中文姓名
        /// </summary>
        /// <param name="count">生成个数</param>
        /// <returns></returns>
        public static List<string> GenChinesePersonName(int count)
        {
            throw new NotImplementedException();
        }
    }
}
