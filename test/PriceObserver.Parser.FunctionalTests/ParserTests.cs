﻿using System;
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
        "https://auchan.zakaz.ua/uk/products/auchan02203766000000/file-nasha-riaba-ukrayina/",
        ShopKey.Auchan,
        "Філе стегна курки Наша ряба курчати-бройлера охолоджене (упаковка PET ~ 1,1кг)",
        "https://img2.zakaz.ua/ryaba.1613125334.ad72436478c_2021-02-12_Tatyana_L/ryaba.1613125334.SNCPSG10.obj.0.1.jpg.oe.jpg.pf.jpg.350nowm.jpg.350x.jpg")]
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
        "https://rozetka.com.ua/philips_75pus8506_12/p306981453/",
        ShopKey.Rozetka,
        "Телевизор Philips 75PUS8506/12",
        "https://content2.rozetka.com.ua/goods/images/big/190084962.jpg")]
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
    public async Task WhenItemIsAvailableAndHasInfo_ShouldParsePage(
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
    }
}