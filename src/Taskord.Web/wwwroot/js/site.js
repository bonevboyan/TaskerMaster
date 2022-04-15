let acceptInviteBtns = document.querySelectorAll('.acceptTeamInviteBtn');
let declineInviteBtns = document.querySelectorAll('.declineTeamInviteBtn');

let acceptFriendBtns = document.querySelectorAll('.acceptFriendBtn');
let declineFriensBtns = document.querySelectorAll('.declineFriendBtn');

let withdrawFriendBtns = document.querySelectorAll('.withdrawFriendBtn');

[...acceptInviteBtns].forEach(x => {
    x.addEventListener('click', e => {
        respondToInvite(x.id, true, x, 'Received Team Invites');
    });
});

[...declineInviteBtns].forEach(x => {
    x.addEventListener('click', e => {
        respondToInvite(x.id, false, x, 'Received Team Invites');
    });
});

[...acceptFriendBtns].forEach(x => {
    x.addEventListener('click', e => {
        respondToRequest(x.id, true, x, 'Received Friend Requests');
    });
});

[...declineFriensBtns].forEach(x => {
    x.addEventListener('click', e => {
        respondToRequest(x.id, false, x, 'Received Friend Requests');
    });
});

[...withdrawFriendBtns].forEach(x => {
    x.addEventListener('click', e => {
        withdrawRequest(x.id, x, 'Sent Friend Requests');
    });
});


async function respondToInvite(id, state, btn, str) {
    const response = await fetch('/api/teams/respondToInvite', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ TeamInviteId: id, IsAccepted: state })
    });

    editDOM('teamInviteCount', response, btn, str, getTeamInvites);
}

async function respondToRequest(id, state, btn, str) {
    const response = await fetch('/api/me/respondToFriendRequest', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ UserId: id, IsAccepted: state })
    });

    editDOM('receivedFriendRequestsCount', response, btn, str, getReceivedRequests);
}

async function withdrawRequest(id, btn, str) {
    const response = await fetch('/api/me/withdrawFriendRequest', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ UserId: id })
    });

    editDOM('sentFriendRequestsCount', response, btn, str, getSentRequests);
}

async function editDOM(countId, response, btn, str, count) {
    if (response.ok) {
        let countLabel = document.getElementById(countId);
        if (countLabel == null) {
            location.reload();
            return;
        };

        btn.parentElement.parentElement.parentElement.remove();

        let number = Number(await count());

        countLabel.textContent = `${str}: ${number}`;
    }
}

async function getReceivedRequests() {
    const response = await fetch('/api/me/receivedRequestsCount', {
        method: 'get',
    });

    let result = await response.text();

    return result;
}

async function getSentRequests() {
    const response = await fetch('/api/me/sentRequestsCount', {
        method: 'get',
    });

    let result = await response.text();
    console.log(result);

    return result;
}

async function getTeamInvites() {
    const response = await fetch('/api/teams/teamInvitesCount', {
        method: 'get',
    });

    let result = await response.text();
    console.log(result);

    return result;
}