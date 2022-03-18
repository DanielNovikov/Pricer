using System;
using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;
using Xunit;

namespace PriceObserver.Parser.Tests.Integration;

public class ParserTests : IntegrationTestingBase
{
    [Theory]
    [InlineData(
        "https://www.adidas.ua/bryuki-yuventus-performance-gr2931",
        ShopKey.Adidas,
        "Брюки Ювентус",
        "https://assetmanagerpim-res.cloudinary.com/images/w_600/q_90/875968816fb7461d9d46acf5010f343a_9366/GR2931_21_model.WebP")]
    [InlineData(
        "https://answear.ua/p/kofta-drykorn-bradley-cholovicha-kolir-zelenyj-gladka-560980",
        ShopKey.Answear,
        "Кофта Drykorn Bradley чоловіча колір зелений гладка",
        "https://img2.ans-media.com/i/540x813/SS22-BLM06T_77X_F1.jpg@jpg?v=1638960396")]
    [InlineData(
        "https://www.brocard.ua/ua/product/ochisniy-zasib-dlya-aksesuariv-dior-backstage-150-ml-140278",
        ShopKey.Brocard,
        "Очисний засіб для аксесуарів Dior Backstage",
        "https://www.brocard.ua/media/catalog/product/cache/39a08324f24a95494ff2c6ca11ab9865/image/2288234191/dior-backstage.jpg")]
    [InlineData(
        "https://www.ctrs.com.ua/electroscooters/detskiy-elektrosamokat-likebike-twist-blue-661359.html",
        ShopKey.Citrus,
        "Электросамокат Like.Bike Twist (Black) 250 Wh",
        "https://i.citrus.world/imgcache/size_180/uploads/shop/d/8/d843364b222194297412ea6d55a9a4ef.png")]
    [InlineData(
        "https://comfy.ua/smartfon-apple-13-pro-512gb-black.html",
        ShopKey.Comfy,
        "Смартфон Apple iPhone 13 Pro 512Gb Graphite",
        "https://cdn.comfy.ua/media/catalog/product/cache/5/image/600x/9df78eab33525d08d6e5fb8d27136e95/i/p/iphone_13_pro_q421_graphite_pdp_image_position-1a__ww-ru_1__2.jpg")]
    [InlineData(
        "https://estore.ua/apple-watch-series-7-45mm-midnight-aluminium-case-with-sport-band/",
        ShopKey.Estore,
        "Apple WATCH Series 7 45mm Midnight Aluminum Case With Midnight Sport Band (MKN53)",
        "https://estore.ua/media/catalog/product/cache/8/image/265x/9df78eab33525d08d6e5fb8d27136e95/a/p/apple-watch-series-7_5__1_1.jpg")]
    [InlineData(
        "https://www.farfetch.com/ua/shopping/men/moschino-iphone-12-item-17412779.aspx?rtype=portal_pdp_outofstock_b&rpos=5&rid=aaf3572d-fa18-4021-a13e-5cb95783f88d",
        ShopKey.Farfetch,
        "Moschino Чехол Для iPhone 12 с Логотипом",
        "https://cdn-images.farfetch-contents.com/17/41/27/79/17412779_36825619_1000.jpg")]
    [InlineData(
        "https://intertop.ua/ua/product/sweaters-and-sweaters-adidas-5701343",
        ShopKey.Intertop,
        "Кофта спортивна Adidas M ZNE WV COLDFZ",
        "https://intertop.ua/load/CN1097/MAIN.jpg")]
    [InlineData(
        "https://makeup.com.ua/product/909452/",
        ShopKey.Makeup,
        "Сыворотка для лица увлажняющая с гиалуроновой кислотой и ниацинамидом - Relance Hyaluronic Acid + Niacinamide Face Serum",
        "https://u.makeup.com.ua/h/he/helk3ldgv9dh.jpg")]
    [InlineData(
        "https://md-fashion.com.ua/store/zenskaa-belaa-futbolka-calvin-klein-performance-00gwf0k142-belyj",
        ShopKey.MdFashion,
        "Женская белая футболка Calvin Klein Performance 00GWF0K142",
        "https://media.md-fashion.com.ua/images/db/56/03da0f3c233b56ecf1ce1e8aafc6.jpg")]
    [InlineData(
        "https://modivo.ua/p/michael-kors-godinnik-pyper-mk4340-zolotii",
        ShopKey.Modivo,
        "Michael Kors Годинник Pyper MK4340 Золотий",
        "https://img.modivo.cloud/product(8/1/a/2/81a2bbd303ea754aedd64cd0a49de870eee03182_mk4340_4013496283877.jpg,jpg)/michael-kors-godinnik-pyper-mk4340-zolotii.jpg")]
    [InlineData(
        "https://www.moyo.ua/televizor-samsung-32t5300-ue32t5300auxua/463569.html",
        ShopKey.Moyo,
        "Телевизор SAMSUNG 32T5300 (UE32T5300AUXUA)",
        "https://img.moyo.ua/img/products/4635/69_1500.jpg?1647599403")]
    [InlineData(
        "https://rozetka.com.ua/lg_75up75006lc/p292227878/",
        ShopKey.Rozetka,
        "Телевизор LG 75UP75006LC",
        "https://content2.rozetka.com.ua/goods/images/big/251707294.jpg")]
    [InlineData(
        "https://www.sportmaster.ua/catalogitem/krossovki_mugskie_puma_supertec_zero384642p0p01/",
        ShopKey.Sportmaster,
        "Кросівки чоловічі PUMA Supertec Zero",
        "https://cdn.sportmaster.ua/static/i/2000_2000/products/260196/VL926aK1.jpeg")]
    [InlineData(
        "https://stylus.ua/samsung-galaxy-a52s-5g-6128gb-black-a528b-p841157c11256.html",
        ShopKey.Stylus,
        "Смартфон Samsung Galaxy A52s 5G 6/128GB Awesome Black A528B",
        "https://stylus.ua//thumbs/568x568/f7/ed/2113714.jpeg")]
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