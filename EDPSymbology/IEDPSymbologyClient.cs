namespace Refinitiv.EDP.Example.Symbology.Convert
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "12.0.14.0 (NJsonSchema v9.13.18.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial interface IEDPSymbologyClient
    {
        /// <param name="format">Format to be used</param>
        /// <returns>Result</returns>
        /// <exception cref="EDPSymbologyException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Symbology> GetConvertAsync(string universe, System.Collections.Generic.IEnumerable<FieldEnum> to, Format? format);
    
        /// <param name="format">Format to be used</param>
        /// <returns>Result</returns>
        /// <exception cref="EDPSymbologyException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Symbology> GetConvertAsync(string universe,  System.Collections.Generic.IEnumerable<FieldEnum> to, Format? format, System.Threading.CancellationToken cancellationToken);
    
        /// <param name="format">Format to be used</param>
        /// <returns>Result</returns>
        /// <exception cref="EDPSymbologyException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Symbology> PostConvertAsync(ConvertRequest convertRequest, Format? format);
    
        /// <param name="format">Format to be used</param>
        /// <returns>Result</returns>
        /// <exception cref="EDPSymbologyException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Symbology> PostConvertAsync(ConvertRequest convertRequest, Format? format, System.Threading.CancellationToken cancellationToken);
    
        /// <returns>Generic OPTIONS response</returns>
        /// <exception cref="EDPSymbologyException">A server side error occurred.</exception>
        System.Threading.Tasks.Task Convert3Async();
    
        /// <returns>Generic OPTIONS response</returns>
        /// <exception cref="EDPSymbologyException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task Convert3Async(System.Threading.CancellationToken cancellationToken);
    
    }
}