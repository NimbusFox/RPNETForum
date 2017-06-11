declare module server {
	interface registerModel {
		username: string;
		password: string;
		confirmPassword: string;
		email: string;
	}
	interface registerResponseModel {
		username: response;
		password: response;
		email: response;
	}
	interface response {
		Success: boolean;
		Reason: string;
	}
}
