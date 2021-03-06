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
        /// 中文 - 常用姓氏 (单姓)
        /// </summary>
        private static readonly string[] _ChineseFamilySingleName = { "赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯", "陈", "楮", "卫", "蒋", "沈", "韩", "杨", "朱", "秦", "尤", "许", "何", "吕", "施", "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜", "戚", "谢", "邹", "喻", "柏", "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范", "彭", "郎", "鲁", "韦", "昌", "马", "苗", "凤", "花", "方", "俞", "任", "袁", "柳", "酆", "鲍", "史", "唐", "费", "廉", "岑", "薛", "雷", "贺", "倪", "汤", "滕", "殷", "罗", "毕", "郝", "邬", "安", "常", "乐", "于", "时", "傅", "皮", "卞", "齐", "康", "伍", "余", "元", "卜", "顾", "孟", "平", "黄", "和", "穆", "萧", "尹", "姚", "邵", "湛", "汪", "祁", "毛", "禹", "狄", "米", "贝", "明", "臧", "计", "伏", "成", "戴", "谈", "宋", "茅", "庞", "熊", "纪", "舒", "屈", "项", "祝", "董", "梁", "杜", "阮", "蓝", "闽", "席", "季", "麻", "强", "贾", "路", "娄", "危", "江", "童", "颜", "郭", "梅", "盛", "林", "刁", "锺", "徐", "丘", "骆", "高", "夏", "蔡", "田", "樊", "胡", "凌", "霍", "虞", "万", "支", "柯", "昝", "管", "卢", "莫", "经", "房", "裘", "缪", "干", "解", "应", "宗", "丁", "宣", "贲", "邓", "郁", "单", "杭", "洪", "包", "诸", "左", "石", "崔", "吉", "钮", "龚", "程", "嵇", "邢", "滑", "裴", "陆", "荣", "翁", "荀", "羊", "於", "惠", "甄", "麹", "家", "封", "芮", "羿", "储", "靳", "汲", "邴", "糜", "松", "井", "段", "富", "巫", "乌", "焦", "巴", "弓", "牧", "隗", "山", "谷", "车", "侯", "宓", "蓬", "全", "郗", "班", "仰", "秋", "仲", "伊", "宫", "宁", "仇", "栾", "暴", "甘", "斜", "厉", "戎", "祖", "武", "符", "刘", "景", "詹", "束", "龙", "叶", "幸", "司", "韶", "郜", "黎", "蓟", "薄", "印", "宿", "白", "怀", "蒲", "邰", "从", "鄂", "索", "咸", "籍", "赖", "卓", "蔺", "屠", "蒙", "池", "乔", "阴", "郁", "胥", "能", "苍", "双", "闻", "莘", "党", "翟", "谭", "贡", "劳", "逄", "姬", "申", "扶", "堵", "冉", "宰", "郦", "雍", "郤", "璩", "桑", "桂", "濮", "牛", "寿", "通", "边", "扈", "燕", "冀", "郏", "浦", "尚", "农", "温", "别", "庄", "晏", "柴", "瞿", "阎", "充", "慕", "连", "茹", "习", "宦", "艾", "鱼", "容", "向", "古", "易", "慎", "戈", "廖", "庾", "终", "暨", "居", "衡", "步", "都", "耿", "满", "弘", "匡", "国", "文", "寇", "广", "禄", "阙", "东", "欧", "殳", "沃", "利", "蔚", "越", "夔", "隆", "师", "巩", "厍", "聂", "晁", "勾", "敖", "融", "冷", "訾", "辛", "阚", "那", "简", "饶", "空", "曾", "毋", "沙", "乜", "养", "鞠", "须", "丰", "巢", "关", "蒯", "相", "查", "后", "荆", "红", "游", "竺", "权", "逑", "盖", "益", "桓", "公", "晋", "楚", "阎", "法", "汝", "鄢", "涂", "钦", "岳", "帅", "缑", "亢", "况", "后", "有", "琴", "商", "牟", "佘", "佴", "伯", "赏", "墨", "哈", "谯", "笪", "年", "爱", "阳", "佟" };

        /// <summary>
        /// 中文 - 常用姓氏 (复姓)
        /// </summary>
        private static readonly string[] _ChineseFamilyDoubleName = { "万俟", "司马", "上官", "欧阳", "夏侯", "诸葛", "闻人", "东方", "赫连", "皇甫", "尉迟", "公羊", "澹台", "公冶", "宗政", "濮阳", "淳于", "单于", "太叔", "申屠", "公孙", "仲孙", "轩辕", "令狐", "锺离", "宇文", "长孙", "慕容", "鲜于", "闾丘", "司徒", "司空", "丌官", "司寇", "仉", "督", "子车", "颛孙", "端木", "巫马", "公西", "漆雕", "乐正", "壤驷", "公良", "拓拔", "夹谷", "宰父", "谷梁", "段干", "百里", "东郭", "南门", "呼延", "归", "海", "羊舌", "微生", "梁丘", "左丘", "东门", "西门", "南宫" };

        #endregion

        #region 中文 - 常用名字_ChineseSecondName - 女

        /// <summary>
        /// 中文 - 常用名字 - 女
        /// </summary>
        public static readonly string[] _ChineseGivenNameFamale = { "嘉", "琼", "桂", "娣", "叶", "璧", "璐", "娅", "琦", "晶", "妍", "茜", "秋", "珊", "莎", "锦", "黛", "青", "倩", "婷", "姣", "婉", "娴", "瑾", "颖", "露", "瑶", "怡", "婵", "雁", "蓓", "纨", "仪", "荷", "丹", "蓉", "眉", "君", "琴", "蕊", "薇", "菁", "梦", "岚", "苑", "婕", "馨", "瑗", "琰", "韵", "融", "园", "艺", "咏", "卿", "聪", "澜", "纯", "毓", "悦", "昭", "冰", "爽", "琬", "茗", "羽", "希", "宁", "欣", "飘", "育", "滢", "馥", "筠", "柔", "竹", "霭", "凝", "晓", "欢", "霄", "枫", "芸", "菲", "寒", "伊", "亚", "宜", "可", "姬", "舒", "影", "荔", "枝", "思", "丽", "秀", "娟", "英", "华", "慧", "巧", "美", "娜", "静", "淑", "惠", "珠", "翠", "雅", "芝", "玉", "萍", "红", "娥", "玲", "芬", "芳", "燕", "彩", "春", "菊", "勤", "珍", "贞", "莉", "兰", "凤", "洁", "梅", "琳", "素", "云", "莲", "真", "环", "雪", "荣", "爱", "妹", "霞", "香", "月", "莺", "媛", "艳", "瑞", "凡", "佳" };

        #endregion

        #region 中文 - 常用名字_ChineseSecondName - 男

        /// <summary>
        /// 中文 - 常用名字 - 男
        /// </summary>
        public static readonly string[] _ChineseGivenNamedMale = { "涛", "昌", "进", "林", "有", "坚", "和", "彪", "博", "诚", "先", "敬", "震", "振", "壮", "会", "群", "豪", "心", "邦", "承", "乐", "绍", "功", "松", "善", "厚", "庆", "磊", "民", "友", "裕", "河", "哲", "江", "超", "浩", "亮", "政", "谦", "亨", "奇", "固", "之", "轮", "翰", "朗", "伯", "宏", "言", "若", "鸣", "朋", "斌", "梁", "栋", "维", "启", "克", "伦", "翔", "旭", "鹏", "泽", "晨", "辰", "士", "以", "建", "家", "致", "树", "炎", "德", "行", "时", "泰", "盛", "雄", "琛", "钧", "冠", "策", "腾", "伟", "刚", "勇", "毅", "俊", "峰", "强", "军", "平", "保", "东", "文", "辉", "力", "明", "永", "健", "世", "广", "志", "义", "兴", "良", "海", "山", "仁", "波", "宁", "贵", "福", "生", "龙", "元", "全", "国", "胜", "学", "祥", "才", "发", "成", "康", "星", "光", "天", "达", "安", "岩", "中", "茂", "武", "新", "利", "清", "飞", "彬", "富", "顺", "信", "子", "杰", "楠", "榕", "风", "航", "弘" };

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

        #region 英文字母(大写)
        private static readonly string _Symbol = "~!@#$%^&*()_+-=[]{};'\\:\"|,./<>?";
        #endregion

        /// <summary>
        /// 格式类型
        /// </summary>
        public enum CharSchemeEnum : byte
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
            /// <summary>
            /// 特殊字符. eg: ~!@#$%^&*()_+-=[]{};'\:"|,./<>?
            /// </summary>
            Symbol
        }

        /// <summary>
        /// 随机生成时间尺度
        /// </summary>
        public enum DateTimeScaleEnum : byte
        {
            Year,

            Month,

            Day,

            Hour,

            Minute,

            Second,

            Millisecond
        }

        /// <summary>
        /// 根据格式类型组合字符集
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private static string getSource(CharSchemeEnum scheme)
        {
            switch (scheme)
            {
                case CharSchemeEnum.Num:
                    return _Nums;
                case CharSchemeEnum.Char:
                    return _CharsL + _CharsU;
                case CharSchemeEnum.CharL:
                    return _CharsL;
                case CharSchemeEnum.CharU:
                    return _CharsU;
                case CharSchemeEnum.NumChar:
                    return _Nums + _CharsL + _CharsU;
                case CharSchemeEnum.NumCharL:
                    return _Nums + _CharsL;
                case CharSchemeEnum.NumCharU:
                    return _Nums + _CharsU;
                case CharSchemeEnum.Chinese:
                    return _ChinesePopular;
                case CharSchemeEnum.Symbol:
                    return _Symbol;
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
        public static int GenNumber(int min, int max)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(min, max);
        }

        /// <summary>
        /// 生成指定范围内的一个随机整数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="count">生成数量</param>
        /// <returns></returns>
        public static List<int> GenNumberList(int min, int max, int count)
        {
            var result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Random(Guid.NewGuid().GetHashCode()).Next(min, max));
            }

            return result;
        }

        public static long GenNumber(long min, long max)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成定长随机字符串
        /// </summary>
        /// <param name="scheme">格式</param>
        /// <param name="len">结果长度</param>
        /// <returns></returns>
        public static string GenString(CharSchemeEnum scheme, int len)
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
        public static string GenString(CharSchemeEnum scheme, int minlen, int maxlen)
        {
            return GenString(scheme, GenNumber(minlen, maxlen));
        }

        /// <summary>
        /// 生成定长随机字符串列表
        /// </summary>
        /// <param name="scheme">生成格式</param>
        /// <param name="len">生成字符串长度</param>
        /// <param name="count">生成个数</param>
        /// <returns></returns>
        public static List<string> GenStringList(CharSchemeEnum scheme, int len, int count)
        {
            var result = new List<string>();

            for (int i = 0; i < count; i++)
            {
                result.Add(GenString(scheme, len));
            }

            return result;
        }

        /// <summary>
        /// 生成指定长度区间随机字符串列表
        /// </summary>
        /// <param name="scheme">生成格式</param>
        /// <param name="minlen">字符串最小长度</param>
        /// <param name="maxlen">字符串最大长度</param>
        /// <param name="count">生成数量</param>
        /// <returns></returns>
        public static List<string> GenStringList(CharSchemeEnum scheme, int minlen, int maxlen, int count)
        {
            var result = new List<string>();

            for (int i = 0; i < count; i++)
            {
                result.Add(GenString(scheme, minlen, maxlen));
            }

            return result;
        }

        /// <summary>
        /// 生成颜色对象
        /// </summary>
        /// <param name="alpha">颜色 alpha, 默认(最大） 255. 不透明</param>
        /// <returns></returns>
        public static Color GenColor(byte alpha = 255)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            var r = rnd.Next(0, 255);
            var g = rnd.Next(0, 255);
            var b = rnd.Next(0, 255);

            return Color.FromArgb(alpha, r, g, b);
        }

        /// <summary>
        /// 生成随机布尔值
        /// </summary>
        /// <returns></returns>
        public static bool GenBoolean()
        {
            var num = GenNumber(0, 1);

            return num == 1;
        }

        /// <summary>
        /// 生成多个随机布尔值
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<bool> GenBoolean(int count)
        {
            var result = new List<bool>();

            var nums = GenNumberList(0, 2, count);

            for (int i = 0; i < count; i++)
            {
                result.Add(nums[i] == 1);
            }

            return result;
        }

        /// <summary>
        /// 生成随机时间
        /// </summary>
        /// <param name="min">最小时间</param>
        /// <param name="max">最大时间</param>
        /// <returns></returns>
        public static DateTime GenDateTime(DateTimeScaleEnum scale, DateTime? min = null, DateTime? max = null)
        {
            long start = 0;
            long end = 0;

            if (min.HasValue)
                start = min.Value.Ticks;

            if (max.HasValue)
                end = max.Value.Ticks;


            return DateTime.Now;
        }

        /// <summary>
        /// 生成随机时间(多个)
        /// </summary>
        /// <param name="count">个数</param>
        /// <param name="min">最小时间</param>
        /// <param name="max">最大时间</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GenDateTime(DateTimeScaleEnum scale, int count, DateTime? min = null, DateTime? max = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成中文姓名
        /// </summary>
        /// <param name="count">生成个数</param>
        /// <returns></returns>
        public static IEnumerable<string> GenChinesePersonName(int count)
        {
            var familyNames = _ChineseFamilySingleName.Union(_ChineseFamilyDoubleName);
            var givenNames = _ChineseGivenNamedMale.Union(_ChineseGivenNameFamale);

            var result = new List<string>();

            for (int i = 0; i < count; i++)
            {
                var fn = familyNames.ElementAt(GenNumber(0, familyNames.Count()));
                var gn = givenNames.ElementAt(GenNumber(0, givenNames.Count()));

                result.Add(fn + gn);
            }

            return result;
        }


        private static char[] constant =
      {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
      };

        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
    }
}
