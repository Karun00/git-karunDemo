Update ArrivalNotification set VoyageIn = 'NA'
where ((VoyageIn IS NULL OR VoyageIn = '')) AND IsANFinal = 'Y'



Update ArrivalNotification set VoyageOut = 'NA'
where ((VoyageOut IS NULL OR VoyageOut = '')) AND IsANFinal = 'Y'