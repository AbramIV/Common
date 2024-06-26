using AppML;

//Load sample data
var sampleData = new SentimentModel.ModelInput()
{
    Col0 = @"Crust is not good.",
};

//Load model and predict output
var result = SentimentModel.Predict(sampleData);

Console.WriteLine(result);