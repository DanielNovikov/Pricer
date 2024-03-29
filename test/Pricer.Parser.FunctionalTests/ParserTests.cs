﻿using System;
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
        "https://assetmanagerpim-res.cloudinary.com/images/w_600/q_90/875968816fb7461d9d46acf5010f343a_9366/GR2931_21_model.WebP")]
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
        "https://answear.ua/p/kofta-drykorn-bradley-cholovicha-kolir-zelenyj-gladka-560980",
        ShopKey.Answear,
        "Кофта Drykorn Bradley чоловіча колір зелений гладка",
        "https://img..ans-media.com/i/540x813/SS22-BLM06T_77X_F1.jpg@jpg?v=1638960396")]
    [InlineData(
        "https://www.ctrs.com.ua/electroscooters/detskiy-elektrosamokat-likebike-twist-blue-661359.html",
        ShopKey.Citrus,
        "Like.Bike Twist (Black) 250 Wh$",
        "https://i.citrus.world/imgcache/size_180/uploads/shop/d/8/d843364b222194297412ea6d55a9a4ef.png")]
    [InlineData(
        "https://comfy.ua/ua/stiral-naja-mashina-whirlpool-wrbsb-6228-b-ua.html",
        ShopKey.Comfy,
        "Пральна машина Whirlpool WRBSS 6215 W UA",
        "https://cdn.comfy.ua/media/catalog/product/w/r/wrbsb_6228_b_ua_5_.jpg")]
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
        "https://eva.ua/ua/pr300615/",
        ShopKey.Eva,
        "Ультрам'який відновлювальний очищувальний засіб для обличчя Numee Glow Up Start Fresh Probiotics + Electrolytes, 150 мл",
        "https://pwa-api.eva.ua/img/512/512/resize/6/6/668643_1_1635369532.jpg")]
    // [InlineData(
    //     "https://www.farfetch.com/ua/shopping/men/moschino-iphone-12-item-17412779.aspx?rtype=portal_pdp_outofstock_b&rpos=5&rid=aaf3572d-fa18-4021-a13e-5cb95783f88d",
    //     ShopKey.Farfetch,
    //     "Moschino Чехол Для iPhone 12 с Логотипом",
    //     "https://cdn-images.farfetch-contents.com/17/41/27/79/17412779_36825619_1000.jpg")]
    [InlineData(
        "https://ua.iherb.com/pr/megafood-zinc-60-tablets/4074",
        ShopKey.IHerb,
        "MegaFood, цинк, 60 таблеток",
        "https://cloudinary.images-iherb.com/image/upload/f_auto,q_auto:eco/images/mgf/mgf10188/v/33.jpg")]
    [InlineData(
        "https://intertop.ua/ua/product/sweaters-and-sweaters-adidas-5701343",
        ShopKey.Intertop,
        "Кофта спортивна Adidas ZNE WV COLDFZ",
        "https://intertop.ua/load/CN1097/MAIN.jpg")]
    [InlineData(
        "https://jysk.ua/zberigannya/komodi/basic/komod-odby-3shukh-bilyy/siryy",
        ShopKey.Jysk,
        "Комод ODBY 3шух білий/сірий",
        "https://cdn4.jysk.com/getimage/wd2.medium/86137")]
    [InlineData(
        "https://makeup.com.ua/product/909452/",
        ShopKey.Makeup,
        "Сыворотка для лица увлажняющая с гиалуроновой кислотой и ниацинамидом - Relance Hyaluronic Acid + Niacinamide Face Serum",
        "https://u.makeup.com.ua/g/ga/garz1eytjlmi.jpg")]
    [InlineData(
        "https://maudau.com.ua/ru/pyvo-leffe-brune-temne-63-zhb-05-l-478576-md-421156.html",
        ShopKey.MauDau,
        "Пиво Leffe Brune темное, 6,3%, ж/б, 0,5 л (478576)",
        "https://sf-api.maudau.com.ua/img/600/744/resize/catalog/product/R/H/RHsslBsz_1.jpg")]
    [InlineData(
        "https://md-fashion.com.ua/store/bezevaa-kurtka-tommy-hilfiger-x-timberland-tommy-hilfiger-mw0mw20906-bezevyj",
        ShopKey.MdFashion,
        "Бежевая куртка Tommy Hilfiger x Timberland Tommy Hilfiger MW0MW20906",
        "https://media.md-fashion.com.ua/images/04/d1/90e13182331c92acc50d2cba50c4.jpg")]
    [InlineData(
        "https://megamarket.zakaz.ua/uk/products/08421384127534/khamon/",
        ShopKey.MegaMarket,
        "Хамон Loriente Серрано нарізка 100г",
        "https://img..zakaz.ua/ultra.1642181619.ad72436478c_2022-01-16_Julia/ultra.1642181619.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
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
        "https://cdn.notinoimg.com/detail_thumb/prada/8435137753307_01x-o/prada-les-infusions-infusion-mimosa_.jpg")]
    [InlineData(
        "https://novus.zakaz.ua/uk/products/04820185100304/file-somga-norven-180g/",
        ShopKey.Novus,
        "Сьомга Norven слабосолена філе-шматок 180г",
        "https://img..zakaz.ua/src.*SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://www.olx.ua/d/obyavlenie/hp-victus-15-i5-12450h-rtx-3050-ram-8gb-ssd-512gb-144hz-IDQvdxB.html",
        ShopKey.Olx,
        "Hp Victus 15/i5 12450H/RTX 3050/RAM 8GB/SSD 512GB/144Hz",
        "https://ireland.apollo.olxcdn.com/v1/files/h79utzoohbq71-UA/image;s=540x540")]
    [InlineData(
        "https://e-pandora.ua/product/kabluchka_potriyna_spiral",
        ShopKey.Pandora,
        "Каблучка \"Потрійна спіраль\"",
        "https://static.e-pandora.ua/19271/PNGTRPNT_180051C01_RGB.png")]
    [InlineData(
        "https://prom.ua/ua/p1298951388-gibkij-neon-12volt.html",
        ShopKey.Prom,
        "Гнучкий неон 12Вольт Блакитна \"SunLight\"",
        "https://images.prom.ua/4154349117_w2048_h2048_2400_2.png?fresh=1")]
    [InlineData(
        "https://prostor.ua/ru/product/lorena-beauty-gubnaya-pomada-uvlazhnyaushchaya-cl04/",
        ShopKey.Prostor,
        "LORENA beauty губна помада зволожувальна №CL04",
        "https://prostor.ua/content/images/41/283x1000l80mc0/lorenabeautygubnapomadazvolozhuvalnacl04-49896498245517.webp")]
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
        "https://stolychnyi.zakaz.ua/uk/products/iogurt-danon-260g-ukrayina--04820226162995/",
        ShopKey.StolychnyiRynok,
        "Йогурт Danone персик-папайя 2,5% 260г",
        "https://img..zakaz.ua/22092022.1663866936.ad72436478c_2022-09-22_Tatyana_L/22092022.1663866936.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://stylus.ua/google-pixel-7-8128gb-snow-p982694c11256.html?sc_content=22390_r963v1317",
        ShopKey.Stylus,
        "Смартфон Google Pixel 7 8/128GB Snow",
        "https://stylus.ua//thumbs/568x568/d9/37/2545091.jpeg")]
    [InlineData(
        "https://telemart.ua/ua/products/asus-27-tuf-gaming-vg27aq-black/",
        ShopKey.Telemart,
        "Mонітор Asus 27\" TUF Gaming VG27AQ Black",
        "https://img.telemart.ua/185613-488316-product_popup/asus-27-tuf-gaming-vg27aq-black.png")]
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