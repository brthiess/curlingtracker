/* Scoreboard */
var fullScreenScoreboardWidth = 719;


function showEvent(eventId, newMobileUrl){
	$('.competition-list-item').removeClass('active');
	$('.competition-list-item[data-id=' + eventId + ']').addClass('active');
	addLoadingToClass('scores-container');
	makeScoresContainerActive();
	getScoresView(eventId, null, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-container-container').html(viewHtml);
			currentCompetitionId = eventId;
			currentDrawId = $('.scores-container-container [data-draw-id]').attr('data-draw-id');
			updateBackButton(true);
			if (typeof newMobileUrl !=="undefined" && $(window).width() < fullScreenScoreboardWidth){
			    history.pushState({action : "showEvent", eventId: eventId}, "", newMobileUrl);
			}
		}
		removeLoadingFromClass('scores-container');
	});
}

function showGame(gameId, eventId, newMobileUrl){
	makeScoresContainerActive();
	addLoadingToClass('scores-container');
	getGameView(gameId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-info-wrapper').html(viewHtml);
			updateBackButton(false, eventId);
		}
		removeLoadingFromClass('scores-container');
		if (typeof newMobileUrl !=="undefined" && $(window).width() < fullScreenScoreboardWidth){
			history.pushState({action : "showGame", gameId: gameId, eventId: eventId}, "", newMobileUrl);
		}
	});
}

function showDraw(drawId, newMobileUrl){
	addLoadingToClass('scores-container');
	getDrawView(drawId, function(viewHtml, error){
		if (typeof error == 'undefined'){
			$('.scores-games-wrapper').html(viewHtml);
		}
		removeLoadingFromClass('scores-container');
		if(typeof newMobileUrl !== "undefined" && $(window).width() < fullScreenScoreboardWidth){
		    history.replaceState({action : "showDraw", drawId: drawId}, "", newMobileUrl);
		}
	})
}

function updateBackButton(isClosing, eventId){
	var onClickAttr = "";
	var backButton = $("[data-back-button]");
	if (isClosing){
		onClickAttr = "closeScores()";
		backButton.attr("data-back-action", "close");
	}
	else {
		onClickAttr = "showEvent('" + eventId + "');";
		backButton.attr("data-back-action", "back");
	}
	backButton.attr("onclick", onClickAttr);
}

function closeScores(){
	makeScoresContainerInActive();
}

function removeLoadingFromClass(className){
	$('.' + className + ' .loading').remove();
	$('.' + className).css('position', '');
}

function addLoadingToClass(className){
	$('.' + className).append('<div class="loading"><div class="spinner"></div></div>');
}
function makeScoresContainerActive(){
	$('.scores-container-container').addClass('active');
	if ($(window).width() < fullScreenScoreboardWidth){
		$('body').css('overflow', 'hidden');
	}
}

function makeScoresContainerInActive(){
	$('.scores-container-container').removeClass('active');
	$('body').css('overflow', '');
}

function showOnScoreboard(section) {
	$("[data-scoreboard-section]").removeClass('active');
	$("[data-scoreboard-section=" + section + "]").addClass('active');
}

function toggleMobileMenu(element) {
	$('.mobile-nav-container').toggleClass('hidden');
	$('body').toggleClass('overflow-hidden');
	toggleHtml(element, 'Close', 'Menu');
}

function toggleHtml(element, text1, text2) {
	if($(element).html() == text1) {
		$(element).html(text2);
	}
	else {
		$(element).html(text1);
	}
}

window.onpopstate = function(event) {
  handlePopState(event.state);
};



$("body").on("click", "[data-pop-history]", function(event){
	if (history.state != null){
		history.back();
	}
});

$("body").on("click", "[data-pop-all-history]", function(event){
	while (history.state != null){
		history.back();
	}
});

function handlePopState(state) {
	if (state == null){
		closeScores();
	}
	if (state.action == "showDraw") {
		showDraw(state.drawId);
    }
    else if (state.action == "showEvent") {
		showEvent(state.eventId);
	}
	else if (state.action == "showGame") {
		showGame(state.gameId, state.eventId);
	}
}
