using System;
using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;
using Xunit;

namespace PriceObserver.Parser.FunctionalTests;

public class ParserTests : TestBase
{
    [Theory]
    [InlineData(
        "https://www.adidas.ua/bryuki-yuventus-performance-gr2931",
        ShopKey.Adidas,
        "Брюки Ювентус",
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
        "https://img2.zakaz.ua/m.1607611321.ad72436478c_2020-12-10_Svetlana/m.1607611321.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://athletics.kiev.ua/catalogitem/krossovki_mugskie_nike_tanjun812654n06001/",
        ShopKey.Athletics,
        "Кросівки чоловічі Nike Tanjun",
        "https://cdn2.athletics.kiev.ua/static/i/2000_2000/products/236580/XS0VZRg5.jpeg")]
    [InlineData(
        "https://answear.ua/p/kofta-drykorn-bradley-cholovicha-kolir-zelenyj-gladka-560980",
        ShopKey.Answear,
        "Кофта Drykorn Bradley чоловіча колір зелений гладка",
        "https://img2.ans-media.com/i/540x813/SS22-BLM06T_77X_F1.jpg@jpg?v=1638960396")]
    [InlineData(
        "https://www.ctrs.com.ua/electroscooters/detskiy-elektrosamokat-likebike-twist-blue-661359.html",
        ShopKey.Citrus,
        "Электросамокат Like.Bike Twist (Black) 250 Wh",
        "https://i.citrus.world/imgcache/size_180/uploads/shop/d/8/d843364b222194297412ea6d55a9a4ef.png")]
    [InlineData(
        "https://comfy.ua/smartfon-apple-13-pro-1tb-pacific-blue.html?sc_content=22595_r776v1072",
        ShopKey.Comfy,
        "Смартфон Apple iPhone 13 Pro 1Tb Sierra Blue",
        "https://cdn.comfy.ua/media/catalog/product/cache/5/image/600x/9df78eab33525d08d6e5fb8d27136e95/i/p/iphone_13_pro_q421_sierra_blue_pdp_image_position-1a__ww-ru_1__1.jpg")]
    [InlineData(
        "https://eko.zakaz.ua/uk/products/04820212490279/khlib-kiyivkhlib-350g/",
        ShopKey.EkoMarket,
        "Хліб Київхліб Супер тост світлий нарізаний 350г",
        "https://img3.zakaz.ua/src.1626182394.ad72436478c_2021-07-13_Tatiana/src.1626182394.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
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
        "https://intertop.ua/ua/product/sweaters-and-sweaters-adidas-5701343",
        ShopKey.Intertop,
        "Кофта спортивна Adidas M ZNE WV COLDFZ",
        "https://intertop.ua/load/CN1097/MAIN.jpg")]
    [InlineData(
        "https://jysk.ua/zberigannya/komodi/basic/komod-odby-3shukh-bilyy/siryy",
        ShopKey.Jysk,
        "Комод ODBY 3шух білий/сірий",
        "https://cdn4.jysk.com/getimage/wd2.medium/86137")]
    [InlineData(
        "https://makeup.com.ua/product/909452/",
        ShopKey.Makeup,
        "Сыворотка для лица увлажняющая с гиалуроновой кислотой и ниацинамидом - Relance Hyaluronic Acid + Niacinamide Face Serum",
        "https://u.makeup.com.ua/h/he/helk3ldgv9dh.jpg")]
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
        "https://img2.zakaz.ua/ultra.1642181619.ad72436478c_2022-01-16_Julia/ultra.1642181619.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://modivo.ua/p/michael-kors-godinnik-pyper-mk4340-zolotii",
        ShopKey.Modivo,
        "Michael Kors Годинник Pyper MK4340 Золотий",
        "https://img.modivo.cloud/product(8/1/a/2/81a2bbd303ea754aedd64cd0a49de870eee03182_mk4340_4013496283877.jpg,jpg)/michael-kors-godinnik-pyper-mk4340-zolotii.jpg")]
    [InlineData(
        "https://www.moyo.ua/televizor-samsung-32t5300-ue32t5300auxua/463569.html",
        ShopKey.Moyo,
        "Телевізор SAMSUNG 32T5300 (UE32T5300AUXUA)",
        "https://img.moyo.ua/img/products/4635/69_1500.jpg?1656671891")]
    [InlineData(
        "https://novus.zakaz.ua/uk/products/04820185100304/file-somga-norven-180g/",
        ShopKey.Novus,
        "Сьомга Norven філе-шматок слабосолена 180г",
        "https://img3.zakaz.ua/src.1644508583.ad72436478c_2022-02-10_Tatiana/src.1644508583.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://e-pandora.ua/product/kabluchka_potriyna_spiral",
        ShopKey.Pandora,
        "Каблучка \"Потрійна спіраль\"",
        "https://static.e-pandora.ua/19271/PNGTRPNT_180051C01_RGB.png")]
    [InlineData(
        "https://rozetka.com.ua/310759363/p310759363/",
        ShopKey.Rozetka,
        "Робот - пылесос ECOVACS DEEBOT OZMO N8 PLUS (DLN26)",
        "https://content.rozetka.com.ua/goods/images/big/195049341.jpg")]
    [InlineData(
        "https://stolychnyi.zakaz.ua/uk/products/04820226160991/iogurt-danon-260g-ukrayina/",
        ShopKey.StolychnyiRynok,
        "Йогурт Danone ананас-манго 2,5% 260г",
        "https://img3.zakaz.ua/src.1603876883.ad72436478c_2020-10-28_Tatyana/src.1603876883.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    // [InlineData(
    //     "https://stylus.ua/samsung-galaxy-a52s-5g-6128gb-black-a528b-p841157c11256.html",
    //     ShopKey.Stylus,
    //     "Смартфон Samsung Galaxy A52s 5G 6/128GB Awesome Black A528B",
    //     "https://stylus.ua//thumbs/568x568/f7/ed/2113714.jpeg")]
    [InlineData(
        "https://telemart.ua/products/evolve-optipart-gold-7h-evop-g7h104fn305x-16s500h1tbk-black/",
        ShopKey.Telemart,
        "Компьютер EVOLVE OptiPart Silver 5H (EVOP-S5Hi104FN305-16S500H1TBk) Black",
        "https://img.telemart.ua/402351-566625-product_popup/evolve-optipart-gold-7h-evop-g7h104fn305x-16s500h1tbk-black.png")]
    [InlineData(
        "https://ultramarket.zakaz.ua/uk/products/04820245520530/sir-premialle-230g/",
        ShopKey.UltraMarket,
        "Сир Premialle Бринза 35% 230г",
        "https://img3.zakaz.ua/upload.version_1.0.224c37aabcbe514772450b8a5d4a2282.350x350.jpeg")]
    [InlineData(
        "https://varus.zakaz.ua/uk/products/varus02050568200006/tsukor-varto-1000g/",
        ShopKey.Varus,
        "Цукор Varto бiлий кристалiчний 1кг",
        "https://img3.zakaz.ua/09.1600792577.ad72436478c_2020-09-22_YuliaT/09.1600792577.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
    [InlineData(
        "https://www.watsons.ua/uk/wtcua/aksesuari/shkarpetki/zhinochi-shkarpetki/shkarpetki-brestskie-active-svitlo-zhovti-zhinochi-rozmir-25/p/BP_1089863?#ins_sr=eyJwcm9kdWN0SWQiOiIxMDg5ODYzIn0=",
        ShopKey.Watsons,
        "Шкарпетки жіночі Брестские Active розмір 25 Світло-жовті 1 шт",
        "https://www.watsons.ua/medias/sys_master/front-prd/front-prd/8862472241182/-Active-25-1-4810089319407.jpg")]
    public async Task WhenPageHasItemInfoAndItemIsInStock_ShouldParsePageAndSetAvailabilityCorrectly(
        string url,
        ShopKey shopKey,
        string expectedTitle,
        string expectedImageUrl)
    {
        var uri = new Uri(url);

        var result = await Parser.Parse(uri, shopKey);

        result.IsSuccess.Should().BeTrue();
        result.Result.ShopKey.Should().Be(shopKey);
        result.Result.Price.Should().NotBe(0);
        result.Result.Title.Should().Be(expectedTitle);
        result.Result.ImageUrl.ToString().Should().Be(expectedImageUrl);
        result.Result.IsAvailable.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("https://allo.ua/ru/televizory/50-xiaomi-mi-tv-uhd-4s-50-international-silver-u1_2.html", ShopKey.Allo)]
    [InlineData("https://auchan.zakaz.ua/uk/products/04823061323897/iogurt-chudo-270g-ukrayina/", ShopKey.Auchan)]
    [InlineData("https://comfy.ua/televizor-lg-43un81006lb.html", ShopKey.Comfy)]
    [InlineData("https://eko.zakaz.ua/uk/products/04820045704536/sir-molokiia-350g-ukrayina/", ShopKey.EkoMarket)]
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
    [InlineData("https://rozetka.com.ua/lg_75nano756pa/p292219333/", ShopKey.Rozetka)]
    //[InlineData("https://stylus.ua/lg-55nano77-p803303c526.html", ShopKey.Stylus)]
    [InlineData("https://stolychnyi.zakaz.ua/uk/products/stolychnyi02010000295007/iogurt-500ml/", ShopKey.StolychnyiRynok)]
    [InlineData("https://telemart.ua/products/lg-315-ultrafine-32un650-w-blacksilver/", ShopKey.Telemart)]
    [InlineData("https://ultramarket.zakaz.ua/uk/products/08410285100050/napii-santal-1000ml/", ShopKey.UltraMarket)]
    [InlineData("https://varus.zakaz.ua/uk/products/04823065726878/iogurt-fanni-280g/", ShopKey.Varus)]
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
}