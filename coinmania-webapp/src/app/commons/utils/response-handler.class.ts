export class ResponseHandler {
    public static success(response: any | undefined) {
        if (!response) {
            return {
                result: { data: null },
            };
        }

        return response;
    }
}
