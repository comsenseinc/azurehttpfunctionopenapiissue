# A minimal repo to demonstrate duplicate key issue in Open Api

There are two classes per [this definition](https://github.com/comsenseinc/azurehttpfunctionopenapiissue/blob/f43577a315a383924b1f3682cd630bb912afa780/FunctionApp1/Function.cs#L41-L83) to form request and response objects in [these two separate Azure Http-triggered functions](https://github.com/comsenseinc/azurehttpfunctionopenapiissue/blob/f43577a315a383924b1f3682cd630bb912afa780/FunctionApp1/Function.cs#L16-L38). Browsing to the Swagger's UI at `http://localhost:7071/api/swagger/ui` leads to the following exception:

![image](https://user-images.githubusercontent.com/72229415/114764918-84fc8780-9d32-11eb-886a-b685bf5937ef.png)

The fully-captured stacktrace is `at System.ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException[T](T key)`

We cannot rename `public MyRequest Request { get; set; }` in both `MySubmittedData` and `YourSubmittedData` classes as it leads to other complexities and breaking the architecture in the code (outside of this repo).
