redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51ITNXHKEWLlDbrAwV9eS4Td34NPrzMi2z9WLMhPec50cVZvMWlOz8hj295ZMrYW8ibOaBP9j88oKiUgTKtuReTV200P1j5Rv1K');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};