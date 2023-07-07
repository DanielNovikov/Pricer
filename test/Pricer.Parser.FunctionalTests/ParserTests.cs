using System;
using System.Threading.Tasks;
using FluentAssertions;
using Pricer.Data.InMemory.Models.Enums;
using Xunit;

namespace Pricer.Parser.FunctionalTests;

public class ParserTests : TestBase
{
    [Theory]
    [InlineData(
        "https://www.adidas.ua/bryuki-yuventus-performance-gr2931",
        ShopKey.Adidas,
        "Штани Ювентус",
        "https://assetmanagerpim-res.cloudinary.com")]
    [InlineData(
        "https://allo.ua/ru/products/mobile/xiaomi-redmi-note-11-4-128-gr-gray-2201117ty.html",
        ShopKey.Allo,
        "Xiaomi Redmi Note 11 4/128 Gr. Gray(2201117TY)",
        "https://i.allo.ua/media/catalog/product/cache/1/image/468x468/602f0fa2c1f0d1ba5e241f914e856ff9/k/7/k724ef_1gfghj_2_1_1.jpg")]
    [InlineData(
        "https://ultramarket.zakaz.ua/uk/products/08423352400045/napii-natru-1000ml/",
        ShopKey.Auchan,
        "Напій рисово-кокосовий Natrue Rice+Coconut без додавання цукру 2% 1л",
        "https://img..zakaz.ua/m.1607611321.ad72436478c_2020-12-10_Svetlana/m.1607611321.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://athletics.kiev.ua/catalogitem/krossovki_mugskie_nike_tanjun812654n06001/",
        ShopKey.Athletics,
        "Кросівки чоловічі Nike Tanjun",
        "https://static.athletics.kiev.ua/static/i/2000_2000/products/236580/XS0VZRg5.jpeg")]
    [InlineData(
        "https://answear.ua/p/zamshevi-shlopantsi-birkenstock-1025788-cholovichi-kolir-zelenyj-boston-983379",
        ShopKey.Answear,
        "Замшеві шльопанці Birkenstock 1025788",
        "https://img2.ans-media")]
    [InlineData(
        "https://www.ctrs.com.ua/electroscooters/detskiy-elektrosamokat-likebike-twist-blue-661359.html",
        ShopKey.Citrus,
        "Like.Bike Twist (Black) 250 Wh$",
        "https://i.citrus.world/imgcache/size_180/uploads/shop/d/8/d843364b222194297412ea6d55a9a4ef.png")]
    [InlineData(
        "https://auto.ria.com/uk/auto_porsche_911_31782126.html",
        ShopKey.AutoRia,
        "Porsche 911 2007",
        "https://cdn4.riastatic.com/photosnew/auto/photo/porsche_911__448779459f.jpg")]
    [InlineData(
        "https://comfy.ua/ua/stiral-naja-mashina-whirlpool-wrbsb-6228-b-ua.html",
        ShopKey.Comfy,
        "Пральна машина Whirlpool WRBSB 6228 B UA",
        "https://cdn.comfy.ua/media/catalog/product/w/r/wrbsb_6228_b_ua_5_.jpg")]
    [InlineData(
        "https://deezee.eu/uk/vechirni-sumki/sribna-vechirnya-sumka-florance-deezee-ccc",
        ShopKey.DeeZee,
        "Срібна вечірня сумка Florance DeeZee&CCC",
        "https://deezee.eu/img/imagecache/32001-33000/157b8e3ff0b91b0414c18557651f35e8622a4a81.webp")]
    [InlineData(
        "https://eko.zakaz.ua/uk/products/04820212490279/khlib-kiyivkhlib-350g/",
        ShopKey.EkoMarket,
        "Хліб Київхліб Супер тост світлий нарізаний 350г",
        "https://img..zakaz.ua/src.1656933913.ad72436478c_2022-08-31_Tatiana/src.1656933913.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://epicentrk.ua/ua/shop/mysh-esperanza-xm110k-black-.html?sc_content=15474_0",
        ShopKey.Epicentr,
        "Миша ESPERANZA XM110K black",
        "https://cdn.27.ua/799/de/ea/384746_1.jpeg")]
    [InlineData(
        "https://estore.ua/apple-watch-series-7-45mm-midnight-aluminium-case-with-sport-band/",
        ShopKey.Estore,
        "Apple WATCH Series 7 45mm Midnight Aluminum Case With Midnight Sport Band (MKN53)",
        "https://estore.ua/media/catalog/product/cache/8/image/265x/9df78eab33525d08d6e5fb8d27136e95/a/p/apple-watch-series-7_5__1_1.jpg")]
    [InlineData(
        "https://eva.ua/ua/pr534627/",
        ShopKey.Eva,
        "Лосьйон для тіла Biolaven Body Lotion Виноград та лаванда",
        "https://pwa-api.eva.ua")]
    // [InlineData(
    //     "https://www.farfetch.com/ua/shopping/men/moschino-iphone-12-item-17412779.aspx?rtype=portal_pdp_outofstock_b&rpos=5&rid=aaf3572d-fa18-4021-a13e-5cb95783f88d",
    //     ShopKey.Farfetch,
    //     "Moschino Чехол Для iPhone 12 с Логотипом",
    //     "https://cdn-images.farfetch-contents.com/17/41/27/79/17412779_36825619_1000.jpg")]
    [InlineData(
        "https://ua.iherb.com/pr/megafood-zinc-60-tablets/4074",
        ShopKey.IHerb,
        "MegaFood, цинк, 60 таблеток",
        "https://cloudinary.images-iherb.com/image/upload/f_auto,q_auto:eco/images/mgf/mgf10188/v/49.jpg")]
    [InlineData(
        "https://intertop.ua/ua/product/sweaters-and-sweaters-adidas-5701343",
        ShopKey.Intertop,
        "Кофта спортивна Adidas ZNE WV COLDFZ",
        "https://intertop.ua/load/CN1097/MAIN.jpg")]
    [InlineData(
        "https://jysk.ua/zberigannya/komodi/komod-tapdrup-5shukh-vuzkyy-dub",
        ShopKey.Jysk,
        "Комод TAPDRUP 5шух",
        @"https:\/\/cdn..jysk.com.+")]
    [InlineData(
        "https://makeup.com.ua/ua/product/994/",
        ShopKey.Makeup,
        "Calvin Klein Eternity For Men",
        "https://u.makeup.com.ua")]
    [InlineData(
        "https://maudau.com.ua/ru/pyvo-leffe-brune-temne-63-zhb-05-l-478576-md-421156.html",
        ShopKey.MauDau,
        "Пиво Leffe Brune, темное, 6,5%, ж/б, 0,5 л (478576)",
        "https://sf-api.maudau.com.ua")]
    [InlineData(
        "https://md-fashion.com.ua/store/zenskie-bosonozki-golden-webbing-wedge-tommy-hilfiger-fw0fw07089-raznocvetnyj",
        ShopKey.MdFashion,
        "Женские босоножки GOLDEN WEBBING WEDGE",
        "https://media.md-fashion.com.ua")]
    [InlineData(
        "https://megamarket.zakaz.ua/uk/products/08421384127534/khamon/",
        ShopKey.MegaMarket,
        "Хамон Loriente Серрано нарізка 100г",
        "https://img..zakaz.ua/ultra.1642181619.ad72436478c_2022-01-16_Julia/ultra.1642181619.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://www.miraton.ua/catalog/outlet/outlet_men/botinki_helly_hansen_000164245/",
        ShopKey.Miraton,
        "Helly Hansen - Чоловічі зелені черевики із нубуку",
        "https://www.miraton.ua")]
    [InlineData(
        "https://modivo.ua/p/michael-kors-godinnik-pyper-mk4340-zolotii",
        ShopKey.Modivo,
        "Michael Kors Годинник Pyper MK4340 Золотий",
        "https://img.modivo.cloud/product(8/1/a/2/81a2bbd303ea754aedd64cd0a49de870eee03182_mk4340_4013496283877.jpg,jpg)/michael-kors-godinnik-pyper-mk4340-zolotii.jpg")]
    [InlineData(
        "https://www.moyo.ua/televizor-samsung-32t5300-ue32t5300auxua/463569.html",
        ShopKey.Moyo,
        "Телевизор Samsung 32T5300 (UE32T5300AUXUA)",
        "https://img.moyo.ua/img/products/4635/69_1500")]
    [InlineData(
        "https://www.notino.ua/prada/les-infusions-infusion-mimosa-parfumovana-voda-uniseks/",
        ShopKey.Notino,
        "Prada Prada Les Infusions: Infusion Mimosa, Парфумовані води 100 мл",
        "https://cdn.notinoimg.com")]
    [InlineData(
        "https://novus.zakaz.ua/uk/products/04820185100304/file-somga-norven-180g/",
        ShopKey.Novus,
        "Сьомга Norven слабосолена філе-шматок 180г",
        "https://img..zakaz.ua/src.*SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://www.olx.ua/d/uk/obyavlenie/kofemashina-necta-krea-IDGu7tz.html?isPreviewActive=0&sliderIndex=4",
        ShopKey.Olx,
        "Кофемашина Necta Krea",
        "https://ireland.apollo.olxcdn.com/v1")]
    [InlineData(
        "https://e-pandora.ua/product/792015c00_e043",
        ShopKey.Pandora,
        "Намистина «Любов до України»",
        "https://static.e-pandora.ua")]
    [InlineData(
        "https://prom.ua/ua/p1298951388-gibkij-neon-12volt.html",
        ShopKey.Prom,
        "Гнучкий неон 12",
        "https://images.prom.ua/")]
    [InlineData(
        "https://prostor.ua/ru/product/maxi-color-pomada-color-show-06-terakot-42g/?sc_content=12822_1",
        ShopKey.Prostor,
        "Помада для губ MAXI color COLOR SHOW №06 Терракот",
        "https://prostor.ua")]
    [InlineData(
        "https://rozetka.com.ua/ua/xiaomi_roborock_s6_maxv/p249086106/",
        ShopKey.Rozetka,
        "Робот-пилосос Roborock S6 MaxV Vacuum Cleaner Black",
        "https://content1.rozetka.com.ua/goods/images/big/32011483.jpg")]
    [InlineData(
        "https://shafa.ua/women/zhenskaya-obuv/botinki/107757836-botinki-ot-ego",
        ShopKey.Shafa,
        "Ботинки от ego",
        "https://image-thumbs.shafastatic.net/681691841_310_430")]
    [InlineData(
        "https://stolychnyi.zakaz.ua/uk/products/iogurt-mlekovita-350g--05900512990095/",
        ShopKey.StolychnyiRynok,
        "Йогурт Mlekovita Персик та Маракуйя 2,5% 350г",
        "https://img..zakaz.ua")]
    [InlineData(
        "https://stylus.ua/google-pixel-7-8128gb-snow-p982694c11256.html?sc_content=22390_r963v1317",
        ShopKey.Stylus,
        "Смартфон Google Pixel 7 8/128GB Snow",
        "https://stylus.ua//thumbs/568x568/d9/37/2545091.jpeg")]
    [InlineData(
        "https://telemart.ua/ua/products/arctic-z1-basic-aemnt00039a/",
        ShopKey.Telemart,
        "Arctic Z1 Basic (AEMNT00039A)",
        "https://img.telemart.ua/")]
    [InlineData(
        "https://ultramarket.zakaz.ua/uk/products/04820245520530/sir-premialle-230g/",
        ShopKey.UltraMarket,
        "Сир Premialle Бринза 35% 230г",
        "https://img..zakaz.ua/upload.version_1.0.224c37aabcbe514772450b8a5d4a2282.350x350.jpeg")]
    [InlineData(
        "https://varus.zakaz.ua/uk/products/varus02050568200006/tsukor-varto-1000g/",
        ShopKey.Varus,
        "Цукор Varto бiлий кристалiчний 1кг",
        "https://img..zakaz.ua/09.1600792577.ad72436478c_2020-09-22_YuliaT/09.1600792577.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://www.watsons.ua/uk/dim/zasobi-dlya-prannya/gel-dlya-delikatnogo-ta-dbaylivogo-prannya-domol-1-5-l/p/BP_183111",
        ShopKey.Watsons,
        "Гель для делікатного та дбайливого прання Domol 1,5 л",
        "https://www.watsons.ua/medias/sys_master/front-prd/front-prd/9004347916318/DOMOL-Domol-1-5-4305615101187.jpg")]
    public async Task WhenPageHasItemInfoAndItemIsInStock_ShouldParsePageAndSetAvailabilityCorrectly(
        string url,
        ShopKey shopKey,
        string expectedTitleRegex,
        string expectedImageUrlRegex)
    {
        var uri = new Uri(url);

        var result = await Parser.Parse(uri, shopKey);

        result.IsSuccess.Should().BeTrue();
        result.Result.ShopKey.Should().Be(shopKey);
        result.Result.Price.Should().NotBe(0);
        result.Result.Title.Should().MatchRegex(FormatRegex(expectedTitleRegex));
        result.Result.ImageUrl.ToString().Should().MatchRegex(FormatRegex(expectedImageUrlRegex));
        result.Result.IsAvailable.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("https://allo.ua/ru/televizory/50-xiaomi-mi-tv-uhd-4s-50-international-silver-u1_2.html", ShopKey.Allo)]
    [InlineData("https://answear.ua/p/kofta-drykorn-bradley-cholovicha-kolir-zelenyj-gladka-560980", ShopKey.Answear)]
    [InlineData("https://auchan.zakaz.ua/uk/products/04823061323897/iogurt-chudo-270g-ukrayina/", ShopKey.Auchan)]
    [InlineData("https://comfy.ua/televizor-lg-43un81006lb.html", ShopKey.Comfy)]
    [InlineData("https://eko.zakaz.ua/uk/products/04820045704536/sir-molokiia-350g-ukrayina/", ShopKey.EkoMarket)]
    [InlineData("https://epicentrk.ua/ua/shop/hubr-meizu-c9-2-16gb-globalnaya-versiya-black.html", ShopKey.Epicentr)]
    [InlineData("https://estore.ua/apple-watch-series-3-nike-42mm-gps-space-gray-aluminium-case-with-anthracite-black-nike-sport-band-mtf42", ShopKey.Estore)]
    [InlineData("https://eva.ua/pr163257/", ShopKey.Eva)]
    //[InlineData("https://www.farfetch.com/ua/shopping/men/alexander-mcqueen-iphone-xs-item-14620644.aspx?storeid=9359", ShopKey.Farfetch)]
    [InlineData("https://intertop.ua/ua/product/sneakers-clarks-4965745?tr_pr=analog", ShopKey.Intertop)]
    [InlineData("https://makeup.com.ua/ua/product/812042/", ShopKey.Makeup)]
    [InlineData("https://maudau.com.ua/ru/pyvo-chernihivske-titan-svitle-8-2-l-890070-md-487758.html", ShopKey.MauDau)]
    [InlineData("https://md-fashion.com.ua/store/zenskie-golubye-dzinsy-kiley-replay-wa434r000108-729-goluboj", ShopKey.MdFashion)]
    [InlineData("https://megamarket.zakaz.ua/uk/products/04820178810401/vershki-organik-milk-180g/", ShopKey.MegaMarket)]
    [InlineData("https://www.moyo.ua/televizor-lg-75sm9000pla/448309.html", ShopKey.Moyo)]
    [InlineData("https://novus.zakaz.ua/uk/products/novus02885537000000/sukhofrukti/", ShopKey.Novus)]
    [InlineData("https://e-pandora.ua/product/kabluchka_potriyna_spiral", ShopKey.Pandora)]
    [InlineData("https://prostor.ua/ru/product/maxi-color-pomada-hydra-shine-4.2g/", ShopKey.Prostor)]
    [InlineData("https://rozetka.com.ua/ua/smart-chasy-xiaomi-watch-s1/g44662672/", ShopKey.Rozetka)]
    [InlineData("https://stylus.ua/xiaomi-yi-sport-white-basic-edition-p227758c997.html", ShopKey.Stylus)]
    [InlineData("https://stolychnyi.zakaz.ua/uk/products/stolychnyi02010000295007/iogurt-500ml/", ShopKey.StolychnyiRynok)]
    [InlineData("https://telemart.ua/products/lg-315-ultrafine-32un650-w-blacksilver/", ShopKey.Telemart)]
    [InlineData("https://ultramarket.zakaz.ua/uk/products/energetik-monster-355ml--05060751213062/", ShopKey.UltraMarket)]
    [InlineData("https://varus.zakaz.ua/uk/products/energetik-non-stop-500ml--04820097899167/", ShopKey.Varus)]
    public async Task WhenPageHasItemInfoButItemIsOutOfStock_ShouldParsePageAndSetAvailabilityCorrectly(
        string url,
        ShopKey shopKey)
    {
        var uri = new Uri(url);

        var result = await Parser.Parse(uri, shopKey);

        result.IsSuccess.Should().BeTrue();
        result.Result.ShopKey.Should().Be(shopKey);
        result.Result.Price.Should().Be(0);
        result.Result.IsAvailable.Should().BeFalse();
    }

    private static string FormatRegex(string regex)
    {
        return regex
            .Replace("/", @"\/")
            .Replace("(", "[(]")
            .Replace(")", "[)]")
            .Replace("+", "[+]")
            .Replace("?", "[?]");
    }
}