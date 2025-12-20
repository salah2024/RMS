function sumKolNew(ActivityLength) {
    debugger;
    var result = [];
    JamKolKhDetail = 0;
    JamKolDarsadKhDetail = 0;

    JamKolVarizi = 0;
    JamKolDarsadVarizi = 0;

    JamKolReUseHajm = 0;
    JamKolReUseDarsad = 0;

    JamKolHaml = 0;
    JamKolDarsadHaml = 0;

    for (let i = 0; i < ActivityLength; i++) {
        currentKhDetail = $.trim($('#txtKhDetail' + (i + 1)).val());
        if (currentKhDetail != '') {
            KhDetail = parseFloat(currentKhDetail);
            KhDetail = isNaN(KhDetail) ? 0 : KhDetail;
            JamKolKhDetail += KhDetail;
        }
        currentDarsadKhDetail = $.trim($('#txtDarsad' + (i + 1)).val());
        if (currentDarsadKhDetail != '') {
            DarsadKhDetail = parseFloat(currentDarsadKhDetail);
            DarsadKhDetail = isNaN(DarsadKhDetail) ? 0 : DarsadKhDetail;
            JamKolDarsadKhDetail += DarsadKhDetail;
        }
        currentVarizi = $.trim($('#txtVarizi' + (i + 1)).val());
        if (currentVarizi != '') {
            Varizi = parseFloat(currentVarizi);
            Varizi = isNaN(Varizi) ? 0 : Varizi;
            JamKolVarizi += Varizi;
        }
        
        currentReUseHajm = $.trim($('#txtReUseHajm' + (i + 1)).val());
        if (currentReUseHajm != '') {
            ReUseHajm = parseFloat(currentReUseHajm);
            ReUseHajm = isNaN(ReUseHajm) ? 0 : ReUseHajm;
            JamKolReUseHajm += ReUseHajm;
        }

        currentHaml = $.trim($('#txtHaml' + (i + 1)).val());
        if (currentHaml != '') {
            Haml = parseFloat(currentHaml);
            Haml = isNaN(Haml) ? 0 : Haml;
            JamKolHaml += Haml;
        }
       
    }
    result.push(JamKolKhDetail);
    result.push(JamKolDarsadKhDetail);
    result.push(JamKolVarizi);
    result.push(JamKolReUseHajm);
    result.push(JamKolHaml);

    return result;
}