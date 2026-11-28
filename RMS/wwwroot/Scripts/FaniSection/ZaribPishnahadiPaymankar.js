function ShowZaribPP(OperationId, event) {
    if (event) {
        event.stopPropagation();
        event.preventDefault();
    }


    let str = `
`;

    // ست کردن HTML در div مربوط به OperationId
    $('#ula' + OperationId).html(str);

}
