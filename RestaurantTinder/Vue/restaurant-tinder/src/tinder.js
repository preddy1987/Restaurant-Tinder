export default {
    /**
     * Returns the liked resturant from local storage.
     */
    getLiked: function() {
      try {
        return localStorage.getItem('liked');
      } catch {
        return null;
      }
    },
    /**
     * Parses and saves the liked resturant.
     * @param {Array} token A JWT encoded token.
     */
    saveLiked(array) {
      localStorage.setItem('liked', array);
    },
    /**
     * Invalidates the local liked resturant.
     */
    destroyLiked() {
      localStorage.removeItem('liked');
    },

        /**
     * Returns the liked resturant from local storage.
     */
    getRejected: function() {
        try {
          return localStorage.getItem('liked');
        } catch {
          return null;
        }
      },
      /**
       * Parses and saves the liked resturant.
       * @param {Array} token A JWT encoded token.
       */
      saveRejected(array) {
        localStorage.setItem('liked', array);
      },
      /**
       * Invalidates the local liked resturant.
       */
      destroyRejected() {
        localStorage.removeItem('liked');
      },

  };
  