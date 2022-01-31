$(document).ready(function() {

  /* Navigator ------------------------------------------------------------*/

  function scrollFunction() {
    window.onscroll = function() {
      var scrollPos = $(window).scrollTop();
      if (scrollPos > 80 || document.documentElement.scrollTop > 80) {
        console.log(document.documentElement.scrollTop)
        $(".logo").css('width', "");
        $(".logo").css("max-width", "100px")
        $("nav").css("height", "90px");
        $("nav").css("line-height", "60px");
        $(".dehaze").css("line-height", "80px");
        $(".nav").css("background-color", "#303030")
        $(".links, .dehaze").css("color", "white")
        $(".logopic").attr("src", "img/logo_transparent_invert.png")
        $(".logopicproduct").attr("src", "../img/logo_transparent_invert.png")
      } else {
        $(".logo").css('width', "");
        $(".logo").css("max-width", "250px")
        $(".nav").css("height", "125px");
        $(".nav-products").css("height", "175px")
        $("nav, .dehaze").css("line-height", "125px");
        $(".nav").css("background-color", "transparent");
        $(".links, .dehaze").css("color", "black");
        $(".logopic").attr("src", "img/logo_transparent.png");
        $(".logopicproduct").attr("src", "../img/logo_transparent.png")

      }
    }
  }

  if ($(window).width() > 750) {
    scrollFunction();
  } else {
    $(".logo").css('width', "");
    $(".logo").css("max-width", "100px")
    $("nav").css("height", "90px");
    $("nav").css("line-height", "60px");
    $(".dehaze").css("line-height", "80px");
    $(".nav").css("background-color", "#303030")
    $(".links, .dehaze").css("color", "white")
    $(".logopic").attr("src", "img/logo_transparent_invert.png")
    $(".logopicproduct").attr("src", "../img/logo_transparent_invert.png")
    $("nav").css("position", "static")
  }

  $(window).resize(function() {
    if ($(window).width() > 750) {
      scrollFunction();
    } else {
      $(".logo").css('width', "");
      $(".logo").css("max-width", "100px")
      $("nav").css("height", "90px");
      $("nav").css("line-height", "60px");
      $(".dehaze").css("line-height", "80px");
      $(".nav").css("background-color", "#303030")
      $(".links, .dehaze").css("color", "white")
      $(".logopic").attr("src", "img/logo_transparent_invert.png")
      $("nav").css("position", "static")
      $(".logopicproduct").attr("src", "../img/logo_transparent_invert.png")
    }
  });

  $(".dehazeicon").click(function() {
    $(".nav-slide-down").fadeToggle("slow");
  });
  /*---------------------------------------------------------------------*/

  /* Slidshows ------------------------------------------------------------*/

  $("#slideshow > div:gt(0)").hide();
  setInterval(function() {
    $('#slideshow > div:first')
      .fadeOut(3000)
      .next()
      .fadeIn(3000)
      .end()
      .appendTo('#slideshow');
  }, 10000);

  if ($(window).width() > 750) {
    $(".banana").slick({
      infinite: true,
      slidesToShow: 3,
      slidesToScroll: 1,
      autoplay: true,
      autoplaySpeed: 5000,
    });
  } else {
    $(".banana").slick({
      infinite: true,
      slidesToShow: 1,
      slidesToScroll: 1,
      autoplay: true,
      autoplaySpeed: 5000,
    });
  }


  /*---------------------------------------------------------------------*/

  /* FAQ -------------------------------------------------------------- */

  $(".faqbutton").click(function() {
    $(this).parent().find("div.faq-inside-faq").slideToggle()
    $(this).parent().toggleClass("fullborder")
    if ($(this).find("div.faq-subsection-plus").html() === "-") {
      $(this).find("div.faq-subsection-plus").html("+")
    } else {
      $(this).find("div.faq-subsection-plus").html("-")
    }
  });

  $(".subfaq").click(function() {
    $(this).parent().find("div.insidefaq").slideToggle()
    $(this).parent().toggleClass("fullborder")
    if ($(this).find("div.faq-subsection-plus").html() === "-") {
      $(this).find("div.faq-subsection-plus").html("+")
    } else {
      $(this).find("div.faq-subsection-plus").html("-")
    }
  });

  /*---------------------------------------------------------------------*/

  /* Webshop --------------------------------------------------------------*/

$(".all").click(function() {
  $(".smoothie").show()
  $(".mixed").show()
  $(".pure").show()
  $(".shot").show()
  $(".juice").show()
});



$(".juicebot").click(function() {
  $(".smoothie").hide()
  $(".mixed").hide()
  $(".pure").hide()
  $(".shot").hide()
  $(".juice").show()
});

$(".smoothiebot").click(function() {
  $(".mixed").hide()
  $(".pure").hide()
  $(".shot").hide()
  $(".juice").hide()
  $(".smoothie").show()

});

$(".mixbot").click(function() {
  $(".smoothie").hide()
  $(".pure").hide()
  $(".shot").hide()
  $(".juice").hide()
  $(".mixed").show()
});

$(".purebot").click(function() {
  $(".smoothie").hide()
  $(".mixed").hide()
  $(".shot").hide()
  $(".juice").hide()
  $(".pure").show()
});

$(".shotbot").click(function() {
  $(".smoothie").hide()
  $(".mixed").hide()
  $(".pure").hide()
  $(".juice").hide()
  $(".shot").show()
});

  /*---------------------------------------------------------------------*/
  /* Compare ----------------------------------------------------------------*/


    $(".guavleft").click(function() {
      $(".left-list").hide()
      $(".guav").show()
    });

    $(".cranleft").click(function() {
      $(".left-list").hide()
      $(".cran").show()
    });

    $(".watleft").click(function() {
      $(".left-list").hide()
      $(".wat").show()
    });

    $(".ginleft").click(function() {
      $(".left-list").hide()
      $(".gin").show()
    });

    $(".rootleft").click(function() {
      $(".left-list").hide()
      $(".root").show()
    });

    $(".mixleft").click(function() {
      $(".left-list").hide()
      $(".mix").show()
    });

    $(".cranright").click(function() {
      $(".right-list").hide()
      $(".crans").show()
    });

    $(".guavright").click(function() {
      $(".right-list").hide()
      $(".guavs").show()
    });

    $(".watright").click(function() {
      $(".right-list").hide()
      $(".wats").show()
    });

    $(".ginright").click(function() {
      $(".right-list").hide()
      $(".gins").show()
    });

    $(".rootright").click(function() {
      $(".right-list").hide()
      $(".roots").show()
    });

    $(".mixright").click(function() {
      $(".right-list").hide()
      $(".mixs").show()
    });

    $(".closecompare").click(function() {
      $(".compare-left").hide()
      $(".left-list").show()
    });

    $(".closecompare-right").click(function() {
      $(".compare-right").hide()
      $(".right-list").show()
    });



  /*---------------------------------------------------------------------*/
  /* Order ----------------------------------------------------------------*/
  $(".guava").keyup().mouseup(function() {
    var num = $(".guava").val();
    $(".guava").text(num);
  });
  $(".tropical").keyup().mouseup(function() {
    var num = $(".tropical").val();
    $(".tropical").text(num);
  });
  $(".cranberry").keyup().mouseup(function() {
    var num = $(".cranberry").val();
    $(".cranberry").text(num);
  });
  $(".ginger").keyup().mouseup(function() {
    var num = $(".ginger").val();
    $(".ginger").text(num);
  });
  $(".fresh").keyup().mouseup(function() {
    var num = $(".fresh").val();
    $(".fresh").text(num);
  });
  $(".rootbeer").keyup().mouseup(function() {
    var num = $(".rootbeer").val();
    $(".rootbeer").text(num);
  });

$(":input").keyup().mouseup(function() {
  var guava = $(".guava").val()
  var tropical = $(".tropical").val()
  var cranberry = $(".cranberry").val()
  var ginger = $(".ginger").val()
  var fresh = $(".fresh").val()
  var rootbeer = $(".rootbeer").val()
  var total = guava * 99 + tropical * 129 + cranberry * 69 + ginger * 149 + fresh * 99 + rootbeer * 129
  $(".total").text(total+"kr")
});



  /* ------------------------------------------------------------------------*/


});
