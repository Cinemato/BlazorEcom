window.ShowToastr = (type, message) => {
	switch (type.toLowerCase()) {
		case "success": toastr.success(message); break;
		case "error": toastr.error(message); break;
	}
}