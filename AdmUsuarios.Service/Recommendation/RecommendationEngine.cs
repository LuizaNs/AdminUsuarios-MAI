using AdmUsuarios.Models;
using Microsoft.ML;

namespace AdmUsuarios.Service.Recommendation
{
    public class RecommendationEngine
    {
        private readonly MLContext _mlContext = new MLContext();
        private ITransformer _model;

        public void PrepareTrainModel(IEnumerable<CidadeEmpresa> cidadeEmpresas)
        {
            var cidadeFrequencias = new List<CidadeEmpresa>();
            foreach (var cidadeEmpresa in cidadeEmpresas)
            {
                cidadeFrequencias.Add(new CidadeEmpresa
                {
                    CidadeId = cidadeEmpresa.CidadeId,
                    EmpresaId = cidadeEmpresa.EmpresaId,
                    Label = 1 
                });
            }

            Train(cidadeFrequencias);
        }
        private void Train(List<CidadeEmpresa> cidadeFrequencias)
        {
            IDataView trainingData = _mlContext.Data.LoadFromEnumerable(cidadeFrequencias);

            var pipeline = _mlContext.Transforms.Conversion
                .MapValueToKey(outputColumnName: "cidadeIdEncoded", inputColumnName: nameof(CidadeEmpresa.CidadeId))
                .Append(_mlContext.Transforms.Conversion
                    .MapValueToKey(outputColumnName: "empresaIdEncoded", inputColumnName: nameof(CidadeEmpresa.EmpresaId)))
                .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
                    labelColumnName: nameof(CidadeEmpresa.Label),
                    matrixColumnIndexColumnName: "cidadeIdEncoded",
                    matrixRowIndexColumnName: "empresaIdEncoded"));

            _model = pipeline.Fit(trainingData);
        }
        public float Predict(string cidadeId, string empresaId)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<CidadeEmpresa, CidadePrediction>(_model);

            var prediction = predictionEngine.Predict(new CidadeEmpresa
            {
                CidadeId = cidadeId,
                EmpresaId = empresaId
            });

            return prediction.Score;
        }
    }
    class CidadePrediction
    {
        public float Score { get; set; }
    }
}